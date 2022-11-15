using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyWallet.Core;
using MyWallet.Core.Dal;
using MyWallet.Services.Dto;

namespace MyWallet.Services;

public class AccountService : IAccountService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AccountService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AccountDto> CreateAccountAsync(AccountCreateDto account)
    {
        var acc = _mapper.Map<Account>(account);

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

        return await _mapper.ProjectTo<AccountDto>(result).ToListAsync();
    }

    public async Task<AccountDto?> GetAccountAsync(Guid id)
    {
        var result = _context.Accounts
             .AsNoTracking()
             .Where(c => c.Id == id);

        return await _mapper.ProjectTo<AccountDto>(result).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateAccountAsync(AccountDto account)
    {
        var result = false;
        var exists = await _context.Accounts.FindAsync(account.Id);
        if (exists is not null)
        {
            exists.Name = account.Name;
            exists.CurrencySymbol = account.CurrencySymbol.ToUpperInvariant();
            exists.Balance = account.Balance;
            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }
}
