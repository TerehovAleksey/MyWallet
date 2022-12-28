namespace MyWallet.Client.Services.Data;

public interface IDataService
{
    public IAuthService Auth { get; }
    public IAccountService Account { get; }
    public ICurrencyService Currency { get; }
    public IRecordService Record { get; }
    public ICategoryService Categories { get; }
}