using MyWallet.Services.Dto;

namespace MyWallet.Services;

public interface IAccountService
{
    public Task<IEnumerable<AccountDto>> GetAccountsAsync();
    public Task<AccountDto?> GetAccountAsync(Guid id);
    public Task<AccountDto> CreateAccountAsync(AccountCreateDto account);
    public Task<bool> UpdateAccountAsync(AccountDto account);
    public Task<bool> DeleteAccountAsync(Guid id);

}
