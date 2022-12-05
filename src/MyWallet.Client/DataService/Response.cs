namespace MyWallet.Client.DataService;

public class Response<T> : IResponse
{
    public T? Item { get; set; }
    public IReadOnlyList<string> Errors { get; set; } = new List<string>();
    public State State { get; set; } = State.Success;

    public Response()
    {

    }

    public Response(T? item)
    {
        Item = item;
    }

    public Response(State state)
    {
        State = state;
    }

    public Response(State state, IEnumerable<string> errors) : this(state)
    {
        Errors = new List<string>(errors);
    }
}
