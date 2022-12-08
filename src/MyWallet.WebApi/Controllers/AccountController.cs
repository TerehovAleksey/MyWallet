namespace MyWallet.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/account")]
[Produces(MediaTypeNames.Application.Json)]
public class AccountController : BaseApiController
{
    private readonly IAccountService _accountService;
    private readonly ICurrencyService _currencyService;

    public AccountController(IAccountService accountService, ICurrencyService currencyService, UserManager<User> userManager) : base(userManager)
    {
        _accountService = accountService;
        _currencyService = currencyService;
    }

    /// <summary>
    /// Список счетов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetAllAccountsAsync()
    {
        var user = await GetUserAsync();
        if (user is null)
        {
            return Unauthorized();
        }
        
        var result = await _accountService.GetAccountsAsync(user.Id);
        return Ok(result);
    }

    /// <summary>
    /// Получение счёта по ID
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(IEnumerable<AccountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountAsync([FromRoute]Guid id)
    {
        var result = await _accountService.GetAccountAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Добавление счёта
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AccountTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateAccountAsync([FromBody] AccountCreateDto account)
    {
        var user = await GetUserAsync();

        if (user is null)
        {
            return Unauthorized();
        }

        // проверка валюты
        var currency = _currencyService.GetAllSymbolsWithDescription().FirstOrDefault(x => x.Key == account.CurrencySymbol.ToUpperInvariant());
        if (string.IsNullOrEmpty(currency.Value))
        {
            return BadRequest();
        }
        
        var result = await _accountService.CreateAccountAsync(user.Id, account);
        return Created($"api/account/{result.Id}", result);
    }

    /// <summary>
    /// Изменение счёта
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid id, [FromBody] AccountUpdateDto account)
    {
        if (account.Id != id)
        {
            return BadRequest();
        }

        var result = await _accountService.UpdateAccountAsync(account);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Удаление счёта
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccountAsync([FromRoute] Guid id)
    {
        var result = await _accountService.DeleteAccountAsync(id);

        if (result)
        {
            return NoContent();
        }

        return NotFound();
    }
}
