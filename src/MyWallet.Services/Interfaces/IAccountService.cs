namespace MyWallet.Services.Interfaces;

public interface IAccountService
{
    public Task<IEnumerable<AccountDto>> GetAccountsAsync(Guid userId);
    public Task<AccountDto?> GetAccountAsync(Guid id);
    public Task<AccountDto> CreateAccountAsync(Guid userId, AccountCreateDto account);
    public Task<bool> UpdateAccountAsync(AccountUpdateDto account);
    public Task<bool> DeleteAccountAsync(Guid id);

}
