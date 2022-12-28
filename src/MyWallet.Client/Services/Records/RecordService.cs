using RestServiceBase = MyWallet.Client.Services.Rest.RestServiceBase;

namespace MyWallet.Client.Services.Records;

public class RecordService : RestServiceBase, IRecordService
{
    private const string KEY = "records";
    
    public RecordService(HttpClient httpClient, IConnectivity connectivity, IStorageService storageService) : base(httpClient, connectivity, storageService)
    {
    }

    public async Task<List<Record>> GetRecordsAsync()
    {
        var records = await StorageService.LoadFromCache<List<Record>>(KEY);
        if (records is not null)
        {
            return records;
        }

        var startDate = new DateTime(2022, 01, 01);
        var endDate = new DateTime(2024, 01, 01);
        records = await GetAsync<List<Record>>($"journal?startDate={startDate:yyyy-MM-dd}&finishDate={endDate:yyyy-MM-dd}");

        await StorageService.SaveToCache(KEY, records, TimeSpan.FromDays(1));
        return records;
    }

    public async Task CreateRecordAsync(RecordCreate record)
    {
        await SendAsync("journal", record).ConfigureAwait(false);
        await StorageService.DeleteFromCache(KEY).ConfigureAwait(false);
        await StorageService.DeleteFromCache(AccountService.ACCOUNTS_KEY).ConfigureAwait(false);
    }
}