namespace MyWallet.Client.Services.Records;

public interface IRecordService
{
    public Task<List<Record>> GetRecordsAsync();
    public Task CreateRecordAsync(RecordCreate record); 
}