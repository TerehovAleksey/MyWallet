namespace MyWallet.Client.ViewControls.Dialog;

public class DialogSettings
{
    public string Title { get; set; } = default!;
    public string OkButtonText { get; set; } = "СОХРАНИТЬ";
    public string CancelButtonText { get; set; } = "ОТМЕНА";

    public bool IsCancelButtonVisible = true;

    public DialogSettings(string title)
    {
        Title = title;
    }
}
