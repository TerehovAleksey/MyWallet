namespace MyWallet.Services;

public class RecordService : IRecordService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public RecordService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<RecordDto?> CreateRecordAsync(RecordCreateDto record)
    {
        var subCategory = await _context.Subcategories
            .AsNoTracking()
            .Select(x => new { x.Id, x.Name, CategoryName = x.Category.Name })
            .FirstOrDefaultAsync(x => x.Id == record.SubcategoryId);

        var journal = _mapper.Map<Journal>(record);

        if (subCategory is null || journal is null)
        {
            return null;
        }

        var isIncome = await _context.Subcategories
            .AsNoTracking()
            .Where(x => x.Id == record.SubcategoryId)
            .Select(x => x.Category.IsIncome)
            .FirstAsync();

        var account = await _context.Accounts
            .Where(x => x.Id == record.AccountId)
            .FirstOrDefaultAsync();

        if (account is null)
        {
            return null;
        }

        using var transaction = _context.Database.BeginTransaction();
        try
        {
            await _context.Journals.AddAsync(journal);
            await _context.SaveChangesAsync();

            account.Balance = isIncome ? account.Balance += record.Value : account.Balance -= record.Value;
            await _context.SaveChangesAsync();

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            return null;
        }

        var result = _mapper.Map<RecordDto>(journal);
        result = result with { Category = subCategory.CategoryName, Subcategory = subCategory.Name };
        return result;
    }

    public async Task<IEnumerable<RecordDto>> GetRecordsAsync(DateTime startDate, DateTime finishDate)
    {
        var records = _context.Journals
            .AsNoTracking()
            .OrderByDescending(x => x.DateOfCreation)
            .Where(x => x.DateOfCreation >= startDate && x.DateOfCreation <= finishDate);

        var result = await _mapper.ProjectTo<RecordDto>(records).ToListAsync();
        return result;
    }

    public async Task<RecordDto?> GetRecordByIdAsync(Guid id)
    {
        var records = _context.Journals
            .AsNoTracking()
            .Where(x => x.Id == id);

        return await _mapper.ProjectTo<RecordDto>(records).FirstOrDefaultAsync();
    }

    public async Task<bool> UpdateRecordAsync(RecordUpdateDto record)
    {
        var result = false;
        var exists = await _context.Journals.FindAsync(record.Id);
        if (exists is not null)
        {
            exists.SubCategoryId = record.SubcategoryId;
            exists.Value = record.Value;
            exists.Description = record.Description;
            await _context.SaveChangesAsync();
            result = true;
        }

        return result;
    }

    public async Task<bool> DeleteRecordAsync(Guid id)
    {
        bool result;
        try
        {
            var category = _context.Journals.Attach(new Journal { Id = id });
            category.State = EntityState.Deleted;
            result = await _context.SaveChangesAsync() > 0;
        }
        catch (Exception)
        {
            result = false;
        }

        return result;
    }
}