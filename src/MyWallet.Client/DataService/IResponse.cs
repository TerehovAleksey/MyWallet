namespace MyWallet.Client.DataService;

public interface IResponse
{
    IReadOnlyList<string> Errors { get; set; }
    State State { get; set; }
}
