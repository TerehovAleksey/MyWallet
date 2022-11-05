using MyWallet.Client.DataServices;
using MyWallet.Client.Models;

namespace MyWallet.Client.Pages;

[QueryProperty(nameof(Category), "Category")]
public partial class CategoryEditPage : ContentPage
{
    private readonly IDataService _dataService;
    private bool _isNew = true;
	private Category _category;
	private List<Category> _subgroups;

	public Category Category
	{
		get => _category;
		set
		{
			_isNew = value.Id == Guid.Empty;
			if (_isNew)
			{
				DeleteItem.IsEnabled = false;
			}
			_category = value;
			NameCell.Detail = value.Name;
			CategoryImage.Source = string.IsNullOrEmpty(value.IconName) ? "cogs.png" : value.IconName;
			OnPropertyChanged();
		}
	}

	public CategoryEditPage(IDataService dataService)
	{
		InitializeComponent();
        _dataService = dataService;
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();

		SubGroupSection.Clear();

        if (_isNew)
		{
			return;
		}
		// load subgroups
		_subgroups = new List<Category>
		{
			new Category
			{
			   Id = Guid.NewGuid(),
			   Name = "������� �������",
			   IconName = "cogs.png"
            },
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "������� �������",
               IconName = "cogs.png"
            },
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "������������ ���������",
               IconName = "cogs.png"
            },
            new Category
            {
               Id = Guid.NewGuid(),
               Name = "�����",
               IconName = "cogs.png"
            }
        };

		var cells = _subgroups.Select(x => new ImageCell
		{
			Text = x.Name,
			ImageSource = x.IconName
		});

        SubGroupSection.Add(cells);

    }

	private async void NameCell_Tapped(object sender, EventArgs e)
	{
		var title = _isNew ? "����� ���������" : _category.Name;
		var message = _isNew ? "������� ��������" : "�������� ��������";
        string name = await DisplayPromptAsync(title, message, placeholder: "��������", keyboard: Keyboard.Text, maxLength: 10);
		if (!string.IsNullOrEmpty(name))
		{
			var result = await _dataService.CreateCategoryAsync(name);
			Category = result;
		}
    }

	private async void DeleteItem_Clicked(object sender, EventArgs e)
	{
		var result = await DisplayAlert("���������", "������� ���������?", "��", "���");
		if (result)
		{
			//TODO
		}
	}
}