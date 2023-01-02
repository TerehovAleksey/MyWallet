namespace MyWallet.Client.ViewControls.Dialog;

public partial class RadioDialog : VerticalStackLayout, IDialog
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
                var items = _parameters!.Get<string[]>("Items");
                var selected = _parameters!.Get<string>("SelectedItem");
                CreateRadio(items, selected);
            }
        }
    }
    
    public RadioDialog()
    {
        InitializeComponent();
    }

    private void CreateRadio(IReadOnlyList<string> items, string? selectedItem)
    {
        for (var i = 0; i < items.Count; i++)
        {
            var radioButton = new RadioButton
            {
                Content = items[i]
            };

            if (i == 0 && string.IsNullOrEmpty(selectedItem) || items[i] == selectedItem)
            {
                radioButton.IsChecked = true;
            }

            radioButton.CheckedChanged += RadioButton_CheckedChanged;
            Container.Add(radioButton);
        }
    }

    private void RadioButton_CheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        //TODO: dev
    }

    //TODO: IDisposable
}