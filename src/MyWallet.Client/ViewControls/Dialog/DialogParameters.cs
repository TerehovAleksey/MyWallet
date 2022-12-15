namespace MyWallet.Client.ViewControls.Dialog;

public class DialogParameters : IEnumerable<KeyValuePair<string, object>>
{
    internal readonly Dictionary<string, object> Parameters;

    public DialogParameters()
    {
        Parameters = new Dictionary<string, object>();
    }

    public void Add(string parameterName, object value)
        => Parameters[parameterName] = value;

    public T Get<T>(string parameterName)
    {
        if (Parameters.TryGetValue(parameterName, out var value))
        {
            return (T)value;
        }

        throw new KeyNotFoundException($"{parameterName} does not exist in modal parameters");
    }

    public T? TryGet<T>(string parameterName)
    {
        if (Parameters.TryGetValue(parameterName, out var value))
        {
            return (T)value;
        }

        return default;
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => Parameters.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Parameters.GetEnumerator();
}
