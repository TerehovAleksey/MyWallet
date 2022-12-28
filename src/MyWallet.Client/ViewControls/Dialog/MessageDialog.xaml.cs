namespace MyWallet.Client.ViewControls.Dialog;

public partial class MessageDialog : VerticalStackLayout, IDialog
{
    private DialogParameters? _parameters;
    public DialogParameters? Parameters
    {
        get => _parameters;
        set
        {
            if (_parameters != value)
            {
                _parameters = value;
                var message = _parameters!.Get<string>("Message");
                TextLabel.Text = message;
            }
        }
    }
    
    public MessageDialog()
    {
        InitializeComponent();
    }
}