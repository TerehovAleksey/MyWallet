using MyWallet.Client.Models;
using MyWallet.Client.ViewModels;

namespace MyWallet.Client.Pages;

[QueryProperty(nameof(Category), "Category")]
public partial class CategoryEditPage : ContentPage
{
	private readonly CategoryEditViewModel _viewModel;
		
	private Category _category;

	public Category Category
	{
		get => _category;
		set
		{
            _category = value;
            _viewModel.SetCategory(value);
		}
	}

	public CategoryEditPage(CategoryEditViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		_viewModel = vm;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		SubGroupSection.Clear();
		var subgroups = await _viewModel.LoadSubGroups();
		var cells = subgroups.Select(x => new ImageCell
		{
			Text = x.Name,
			ImageSource = x.IconName
		});
        SubGroupSection.Add(cells);
    }
}