namespace MyWallet.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/type")]
[Produces(MediaTypeNames.Application.Json)]
public class AccountTypeController : ControllerBase
{
    private const string CacheKey = "AccountTypes";

    private readonly IAccountTypeService _accountTypeService;
    private readonly IMemoryCache _cache;

    public AccountTypeController(IAccountTypeService accountTypeService, IMemoryCache cache)
    {
        _accountTypeService = accountTypeService ?? throw new ArgumentNullException(nameof(accountTypeService));
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    /// <summary>
    /// Список типов счетов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AccountTypeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IEnumerable<AccountTypeDto>> GetAllAccountTypesAsync()
    {
        if (_cache.TryGetValue(CacheKey, out List<AccountTypeDto> result))
        {
            return result;
        }

        result = (await _accountTypeService.GetAccountTypesAsync());

        var memoryCacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromDays(1))
            .SetPriority(CacheItemPriority.Normal);

        _cache.Set(CacheKey, result, memoryCacheEntryOptions);

        return result;
    }

    /// <summary>
    /// Получение типа счёта по ID
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AccountTypeDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountAsync([FromRoute] Guid id)
    {
        var types = await GetAllAccountTypesAsync();
        var result = types.FirstOrDefault(x => x.Id == id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Добавление типа счётов
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(AccountTypeDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateAccountAsync([FromBody] AccountTypeCreateDto dto)
    {
        var result = await _accountTypeService.CreateAccountTypeAsync(dto);
        CleanCache();
        return Created($"api/type/{result.Id}", result);
    }

    /// <summary>
    /// Изменение типа счётов
    /// </summary>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid id, [FromBody] AccountTypeUpdateDto dto)
    {
        if (dto.Id != id)
        {
            return BadRequest();
        }

        var result = await _accountTypeService.UpdateAccountTypeAsync(dto);

        if (!result)
        {
            return NotFound();
        }

        CleanCache();
        return NoContent();
    }

    /// <summary>
    /// Удаление типа счётов
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAccountAsync([FromRoute] Guid id)
    {
        var result = await _accountTypeService.DeleteAccountTypeAsync(id);

        if (result)
        {
            CleanCache();
            return NoContent();
        }

        return NotFound();
    }

    private void CleanCache() => _cache.Remove(CacheKey);
}
