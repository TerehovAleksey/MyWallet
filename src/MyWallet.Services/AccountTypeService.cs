using MyWallet.Core;

namespace MyWallet.Services;

public class AccountTypeService : IAccountTypeService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AccountTypeService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AccountTypeDto> CreateAccountTypeAsync(AccountTypeCreateDto dto)
    {
        var accountType = _mapper.Map<AccountType>(dto);

        if (accountType is not null)
        {
            await _context.AccountTypes.AddAsync(accountType);
            await _context.SaveChangesAsync();
        }

        var result = _mapper.Map<AccountTypeDto>(accountType);
        return result;
    }

    public async Task<bool> DeleteAccountTypeAsync(Guid id)
    {
        bool result;
        try
        {
            var accountType = _context.AccountTypes.Attach(new AccountType { Id = id });
            accountType.State = EntityState.Deleted;
            result = await _context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }

    public async Task<List<AccountTypeDto>> GetAccountTypesAsync()
    {
        var result = _context.AccountTypes
            .AsNoTracking()
            .OrderBy(x => x.Order);

        return await _mapper.ProjectTo<AccountTypeDto>(result).ToListAsync();
    }

    public async Task<bool> UpdateAccountTypeAsync(AccountTypeUpdateDto dto)
    {
        var result = false;
        var exists = await _context.AccountTypes.FindAsync(dto.Id);
        if (exists is not null)
        {
            exists.Name = dto.Name;
            exists.Order = dto.Order;
            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }
}
