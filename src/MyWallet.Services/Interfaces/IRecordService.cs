namespace MyWallet.Services.Interfaces;

public interface IRecordService
{
    public Task<RecordDto?> CreateRecordAsync(RecordCreateDto record);
    public Task<IEnumerable<RecordDto>> GetRecordsAsync(Guid userId, DateTime startDate, DateTime finishDate);
    public Task<RecordDto?> GetRecordByIdAsync(Guid id);
    public Task<bool> UpdateRecordAsync(RecordUpdateDto record);
    public Task<bool> DeleteRecordAsync(Guid id);
}
