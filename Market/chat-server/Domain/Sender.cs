namespace chat_server.Domain
{
    public struct Sender
    {
        public Sender(string value) => Value = value;

        public string Value { get; }
    }
}