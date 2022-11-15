using Microsoft.AspNetCore.Mvc;
using MyWallet.Services;
using MyWallet.Services.Dto;

namespace MyWallet.WebApi.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ICurrencyService _currencyService;

    public AccountController(IAccountService accountService, ICurrencyService currencyService)
    {
        _accountService = accountService;
        _currencyService = currencyService;
    }

    /// <summary>
    /// Список счетов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public Task<IEnumerable<AccountDto>> GetAllAccountsAsync() =>
        _accountService.GetAccountsAsync();

    /// <summary>
    /// Получение счёта по ID
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAccountAsync([FromRoute]Guid id)
    {
        var result = await _accountService.GetAccountAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    /// <summary>
    /// Добавление счёта
    /// </summary>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateAccountAsync([FromBody] AccountCreateDto account)
    {
        var currency = _currencyService.GetAllSimbolsWithDescription().FirstOrDefault(x => x.Key == account.CurrencySymbol.ToUpperInvariant());
        if (string.IsNullOrEmpty(currency.Value))
        {
            return BadRequest();
        }
        var result = await _accountService.CreateAccountAsync(account);
        return Created($"api/account/{result.Id}", result);
    }

    /// <summary>
    /// Изменение счёта
    /// </summary>
    /// <param name="id"></param>
    /// <param name="account"></param>
    /// <returns></returns>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid id, [FromBody] AccountDto account)
    {
        if (account.Id != id)
        {
            return BadRequest();
        }

        var currency = _currencyService.GetAllSimbolsWithDescription().FirstOrDefault(x => x.Key == account.CurrencySymbol.ToUpperInvariant());
        if (string.IsNullOrEmpty(currency.Value))
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
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
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
