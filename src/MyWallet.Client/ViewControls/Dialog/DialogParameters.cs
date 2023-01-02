namespace MyWallet.Client.ViewControls.Dialog;

public class DialogParameters : IEnumerable<KeyValuePair<string, object>>
{
    private readonly Dictionary<string, object> Parameters;

    public DialogParameters()
    {
        Parameters = new Dictionary<string, object>();
    }

    public object this[string key]
    {
        get => Parameters[key];
        set => Parameters[key] = value;
    }

    public void Add(string parameterName, object value)
        => Parameters[parameterName] = value;

    public T Get<T>(string parameterName)
    {
        var value = Parameters[parameterName];
        return (T)value;
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator() => Parameters.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => Parameters.GetEnumerator();
}
