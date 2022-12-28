using System.Windows.Input;

namespace MyWallet.Client.ViewControls.Common;

public partial class PageMenu : Grid
{
    public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create(nameof(SelectedCommand), typeof(ICommand), typeof(PageMenu));
    
    public ICommand SelectedCommand
    {
        get => (ICommand)GetValue(SelectedCommandProperty);
        set => SetValue(SelectedCommandProperty, value);
    }
    
    public static readonly BindableProperty ItemsProperty =
        BindableProperty.Create(nameof(Items), typeof(ObservableCollection<UI.MenuItem>), typeof(PageMenu), null, propertyChanged: OnItemsChanged);

    public ObservableCollection<UI.MenuItem> Items
    {
        get => (ObservableCollection<UI.MenuItem>)GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public static readonly BindableProperty TitleProperty =
        BindableProperty.Create(nameof(Title), typeof(string), typeof(PageMenu), null, propertyChanged: OnTitleChanged);

    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public PageMenu()
    {
        InitializeComponent();
    }

    #region Handlers

    private static void OnItemsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PageMenu pageMenu)
        {
            pageMenu.Collection.ItemsSource = (ObservableCollection<UI.MenuItem>)newValue;
        }
    }

    private static void OnTitleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PageMenu pageMenu)
        {
            pageMenu.TitleLabelContainer.IsVisible = true;
            pageMenu.TitleLabel.Text = newValue.ToString();
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if ((sender as Grid)?.BindingContext is UI.MenuItem item)
        {
            SelectedCommand.Execute(item.Link);
        }
    } 

    #endregion
}