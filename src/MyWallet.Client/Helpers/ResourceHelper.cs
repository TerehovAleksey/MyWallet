namespace MyWallet.Client.Helpers;

public class ResourceHelper
{
    public static Color GetColor(string key)
    {
        var theme = App.Current!.RequestedTheme;

        ResourceDictionary? dictionary;
        switch (theme)
        {
            case AppTheme.Dark:
                dictionary = GetDarkThemeDictionary();
                break;
            default:
                dictionary = GetLightThemeDictionary();
                break;
        }

        if (dictionary is not null)
        {
            var success = dictionary.TryGetValue(key, out object? result);
            if(success)
            {
                return (Color)result;
            }
        }

        return Colors.Transparent;
    }

    public static ResourceDictionary? GetLightThemeDictionary()
    {
        ICollection<ResourceDictionary> mergedDictionaries = App.Current!.Resources.MergedDictionaries;
        return mergedDictionaries.FirstOrDefault(x => x.GetType() == typeof(LightTheme) ||
                                              (x.Source?.ToString().Contains("Colors.LightTheme") ?? false));
    }
    public static ResourceDictionary? GetDarkThemeDictionary()
    {
        ICollection<ResourceDictionary> mergedDictionaries = App.Current!.Resources.MergedDictionaries;
        return mergedDictionaries.FirstOrDefault(x => x.GetType() == typeof(DarkTheme) ||
                                              (x.Source?.ToString().Contains("Colors.DarkTheme") ?? false));
    }
}
