using chat_server.Domain;

namespace chat_server_test.Domain
{
    public static class Senders
    {
        public static Sender Bob() => New("Bob");

        public static Sender Alice() => New("Alice");

        public static Sender New(string name) => new Sender(name);
    }
}