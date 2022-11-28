namespace MyWallet.Services;

public interface IAccountTypeService
{
    public Task<List<AccountTypeDto>> GetAccountTypesAsync();
    public Task<AccountTypeDto> CreateAccountTypeAsync(AccountTypeCreateDto dto);
    public Task<bool> UpdateAccountTypeAsync(AccountTypeUpdateDto dto);
    public Task<bool> DeleteAccountTypeAsync(Guid id);
}
