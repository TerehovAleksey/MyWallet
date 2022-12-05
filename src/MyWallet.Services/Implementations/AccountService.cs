namespace MyWallet.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AccountService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AccountDto> CreateAccountAsync(Guid userId, AccountCreateDto account)
    {
        var acc = _mapper.Map<Account>(account);
        acc.UserId = userId;

        if (acc is not null)
        {
            await _context.Accounts.AddAsync(acc);
            await _context.SaveChangesAsync();
        }

        var result = _mapper.Map<AccountDto>(acc);
        return result;
    }

    public async Task<bool> DeleteAccountAsync(Guid id)
    {
        bool result;
        try
        {
            var account = _context.Accounts.Attach(new Account { Id = id });
            account.State = EntityState.Deleted;
            result = await _context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }

    public async Task<IEnumerable<AccountDto>> GetAccountsAsync()
    {
        var result = _context.Accounts
             .AsNoTracking()
             .OrderBy(c => c.Name);

        var accounts = await _mapper.ProjectTo<AccountDto>(result).ToListAsync();
        return accounts;
    }

    public async Task<AccountDto?> GetAccountAsync(Guid id)
    {
        var result = _context.Accounts
             .AsNoTracking()
             .Where(c => c.Id == id);

        return await _mapper.ProjectTo<AccountDto>(result).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAccountAsync(AccountUpdateDto account)
    {
        var result = false;
        var exists = await _context.Accounts.FindAsync(account.Id);
        if (exists is not null)
        {
            exists.Name = account.Name;
            exists.AccountTypeId = account.AccountTypeId;
            exists.Color = account.Color;
            exists.Number = account.Number;
            exists.IsArchived = account.IsArchived;
            exists.IsDisabled = account.IsDisabled;
            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }
}
