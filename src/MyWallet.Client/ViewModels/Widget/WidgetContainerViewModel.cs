using MyWallet.Client.Services.Settings;

namespace MyWallet.Client.ViewModels.Widget;

public class WidgetContainerViewModel
{
	private readonly ISettingsService _settingsService;

	public ObservableCollection<IWidgetViewModel> Widgets { get; }

	public WidgetContainerViewModel(ISettingsService settingsService)
	{
		_settingsService = settingsService;
		Widgets = new ObservableCollection<IWidgetViewModel>();
	}

	public async Task LoadingWidgetsAsync()
	{
		Widgets.Clear();
		
		// получаем настройки и создаём виджеты
		var widgetSettings = await _settingsService.GetMainWidgetsAsync();
		foreach (var setting in widgetSettings)
		{
			var widget = WidgetFabric.CreateWidget(setting);
			Widgets.Add(widget);
		}
		
		// загружаем данные виджетов
		foreach (var widget in Widgets)
		{
		    await widget.LoadingAsync();
		}
	}
}
