﻿namespace MyWallet.Client.ViewControls.Dialog;

public class DialogSettings
{
    public string Title { get; set; } = default!;
    public string OkButtonText { get; set; } = Strings.Ok;
    public string CancelButtonText { get; set; } = Strings.Cancel;

    public bool IsCancelButtonVisible = true;

    public DialogSettings(string title)
    {
        Title = title;
    }
}
