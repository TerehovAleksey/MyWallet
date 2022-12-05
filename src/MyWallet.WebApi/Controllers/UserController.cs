namespace MyWallet.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/user")]
[Produces(MediaTypeNames.Application.Json)]
public class UserController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly IHostEnvironment _environment;
    private readonly ICurrencyService _currencyService;
    private readonly IAccountService _accountService;
    private readonly IAccountTypeService _accountTypeService;
    private readonly ICategoryService _categoryService;

    public UserController(UserManager<User> userManager, ITokenService tokenService, IHostEnvironment environment, IUserService userService,
        ICurrencyService currencyService, IAccountService accountService, IAccountTypeService accountTypeService, ICategoryService categoryService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _environment = environment;
        _userService = userService;
        _currencyService = currencyService;
        _accountService = accountService;
        _accountTypeService = accountTypeService;
        _categoryService = categoryService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] UserForAuthDto userForAuthentication)
    {
        // устройство, с которого логинятся
        var clientInfo = GetClientInfo();
        if (string.IsNullOrWhiteSpace(clientInfo.DeviceName))
        {
            return Unauthorized(ErrorMessage.Create(Strings.UnknownDevice));
        }

        var user = await _userManager.FindByEmailAsync(userForAuthentication.Email);

        // пользователь не найден
        if (user == null)
        {
            return Unauthorized(ErrorMessage.Create(Strings.InvalidAuthentication));
        }

        // подтверждение почты
        if (!(await _userManager.IsEmailConfirmedAsync(user)))
        {
            return Unauthorized(ErrorMessage.Create(Strings.EmailNotConfirmed));
        }

        // пользователь заблокирован
        if (await _userManager.IsLockedOutAsync(user))
        {
            return Unauthorized(ErrorMessage.Create(Strings.LockoutEnabled));
        }

        // неверный пароль
        if (!await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
        {
            await _userManager.AccessFailedAsync(user);
            return Unauthorized(ErrorMessage.Create(Strings.InvalidAuthentication));
        }

        // если всё успешно, генерируем токен
        var token = await _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var accessTokenExpiryTime = DateTime.UtcNow.AddMinutes(_tokenService.AccessTokenExpiration);
        var refreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(_tokenService.RefreshTokenExpiration);

        await _userService.UpdateOrCreateDeviceAsync(user.Id, clientInfo.DeviceName, clientInfo.Ip, refreshToken, refreshTokenExpiryTime);

        await _userManager.UpdateAsync(user);
        await _userManager.ResetAccessFailedCountAsync(user);

        return Ok(new AuthResponseDto(token, refreshToken, accessTokenExpiryTime));
    }

    [HttpPost("registration")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
    {
        //TODO: проверка по email и LockoutEnabled = false после проверки, сейчас true
        //TODO: изменить потом в IdentityConfigurations => options.Lockout.AllowedForNewUsers = false;
        //TODO: тут убрать EmailConfirmed = true
        var user = new User { UserName = userForRegistration.Email, Email = userForRegistration.Email, EmailConfirmed = true };

        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(ErrorMessage.Create(errors));
        }

        result = await _userManager.AddToRoleAsync(user, "User");
        if (!result.Succeeded)
        {
            // есть вероятность, что пользователь будет создан, но не добавлен к роли
            // и будет ошибка, но пользователь будет создан
            // значит удалить пользователя
            await _userManager.DeleteAsync(user);
            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(ErrorMessage.Create(errors));
        }

        // Send Email

        //TODO: подумать, может вынести в визард

        // валюта пользователя
        await _currencyService.CreateUserCurrencyAsync(user.Id, "BYN", true);

        // счёт пользователя
        var types = await _accountTypeService.GetAccountTypesAsync();
        var typeId = types.First(x => x.Name == "Общий").Id;
        await _accountService.CreateAccountAsync(user.Id, new AccountCreateDto("Наличные", null, typeId, 0, "BYN", "#ad1457"));

        // категории и подкатегории
        await _categoryService.InitCategoriesForUser(user.Id);

        //TODO: пока сразу логиним, потом логику изменим
        return await Login(new UserForAuthDto(userForRegistration.Email, userForRegistration.Password));
    }

    [HttpGet("logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Logout()
    {
        var user = await GetUserByClaims();
        var (DeviceName, _) = GetClientInfo();
        if (user != null && !string.IsNullOrEmpty(DeviceName))
        {
            await _userService.DeleteDeviceAsync(user.Id, DeviceName);
        }

        return Ok();
    }

    [HttpPost("password")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorMessage), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePassword([FromBody] PasswordChangeDto model)
    {
        var user = await GetUserByClaims();
        if (user != null)
        {
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }

            var errors = result.Errors.Select(e => e.Description).ToList();
            return BadRequest(ErrorMessage.Create(errors));
        }

        return NotFound();
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto tokenDto)
    {
        // открытый вызов, доп информацию о неудачах обновления токена не даём!

        ClaimsPrincipal? principal;
        try
        {
            principal = _tokenService.GetPrincipalFromExpiredToken(tokenDto.Token);
        }
        catch
        {
            return Unauthorized();
        }

        var username = principal?.Identity?.Name ?? string.Empty;
        var user = await _userManager.FindByEmailAsync(username);
        if (user == null)
        {
            return Unauthorized();
        }

        // получение устройства пользователя
        var (DeviceName, Ip) = GetClientInfo();
        if (string.IsNullOrWhiteSpace(DeviceName))
        {
            return Unauthorized();
        }

        var isRefreshTokenValid = await _userService.CheckTokenAsync(user.Id, DeviceName, tokenDto.RefreshToken);

        if (!isRefreshTokenValid)
        {
            return Unauthorized();
        }

        var token = await _tokenService.GenerateToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var refreshTokenExpiryTime = DateTime.UtcNow.AddMinutes(_tokenService.RefreshTokenExpiration);
        var accessTokenExpiryTime = DateTime.UtcNow.AddMinutes(_tokenService.AccessTokenExpiration);

        await _userService.UpdateOrCreateDeviceAsync(user.Id, DeviceName, Ip, refreshToken, refreshTokenExpiryTime);

        return Ok(new AuthResponseDto(token, refreshToken, accessTokenExpiryTime));
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProfile()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        var user = await _userManager.FindByEmailAsync(email);
        if (user != null)
        {
            var logoPath = string.Empty;
            var imgDirectory = new DirectoryInfo(Path.Combine(_environment.ContentRootPath, "wwwroot", "img"));
            var logoFile = imgDirectory.GetFiles(user.Id.ToString().ToLower() + "*").FirstOrDefault();
            if (logoFile is not null)
            {
                logoPath = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/img/{logoFile.Name}";
            }

            var result = new UserDto(user.FirstName, user.LastName, user.Email ?? string.Empty, user.BirthdayDate, (Gender)user.Gender, logoPath);
            return Ok(result);
        }

        return NotFound();
    }

    [HttpPost("profile")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateProfile([FromBody] UserUpdateDto model)
    {
        var user = await GetUserByClaims();
        if (user != null)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.BirthdayDate = model.BirthdayDate;
            user.Gender = (int)model.Gender;
            await _userManager.UpdateAsync(user);
            return NoContent();
        }

        return NotFound();
    }

    private (string? DeviceName, string? Ip) GetClientInfo()
    {
        var deviceName = Request.Headers.FirstOrDefault(x => x.Key.ToLowerInvariant() == "device-name").Value.ToString();
        var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
        return (deviceName, ip);
    }

    private Task<User?> GetUserByClaims()
    {
        var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? string.Empty;
        return _userManager.FindByEmailAsync(email);
    }
}