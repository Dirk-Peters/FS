namespace chat_server.Domain
{
    public interface ISession
    {
        bool IsValid { get; }
    }
}