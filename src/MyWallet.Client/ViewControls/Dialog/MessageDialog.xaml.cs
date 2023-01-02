namespace MyWallet.Client.ViewControls.Dialog;

public partial class MessageDialog : VerticalStackLayout, IDialog
{
    private DialogParameters? _parameters;
    public DialogParameters? Parameters
    {
        get => _parameters;
        set
        {
            if (value != null && _parameters != value)
            {
                _parameters = value;
                TextLabel.Text = _parameters!.Get<string>("Message");
            }
        }
    }
    
    public MessageDialog()
    {
        InitializeComponent();
    }
}