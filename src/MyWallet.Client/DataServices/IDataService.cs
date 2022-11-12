using MyWallet.Client.Models;

namespace MyWallet.Client.DataServices;

public interface IDataService
{
    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<Category> CreateCategoryAsync(string name, string imageName);

    public Task<List<Record>> GetRecordsAsync(DateTime startDate, DateTime endDate);
    public Task<Record> CreateRecordAsync(RecordCreate record);

}
