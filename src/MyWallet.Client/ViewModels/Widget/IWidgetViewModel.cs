namespace MyWallet.Client.ViewModels.Widget;

public interface IWidgetViewModel
{
    /// <summary>
    /// Id виджета
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Тип виджета
    /// </summary>
    public WidgetType WidgetType { get; set; }

    /// <summary>
    /// Название виджета
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Доп. информация
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Фильтр
    /// </summary>
    public WidgetFilter Filter { get; set; }
    
    /// <summary>
    /// Начальная инициализация виджета
    /// </summary>
    public void Init();
    
    /// <summary>
    /// Начальная (асинхронная) инициализация виджета
    /// </summary>
    /// <returns></returns>
    public Task LoadingAsync();
    
    /// <summary>
    /// Открытие настроек виджета
    /// </summary>
    /// <returns></returns>
    public Task OpenFilterAsync();
    
    /// <summary>
    /// Навигация на статистику
    /// </summary>
    /// <returns></returns>
    public Task OpenDetailsAsync();
    
    /// <summary>
    /// Получение записей (вызывается автоматически при выборе счетов)
    /// </summary>
    /// <param name="records"></param>
    public void SetRecords(IEnumerable<Record> records);
}
