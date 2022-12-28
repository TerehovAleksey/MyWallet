namespace MyWallet.Client.Exceptions;

public class ServerResponseException : Exception
{
    public ServerResponseException()
    {
    }

    public ServerResponseException(string message) : base(message)
    {
    }

    public ServerResponseException(string message, Exception inner) : base(message, inner)
    {
    }
}
