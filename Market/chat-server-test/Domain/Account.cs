using chat_server.Domain;

namespace chat_server_test.Domain
{
    public class Account
    {
        private readonly Password password;
        private readonly string userName;

        public Account(string userName, Password password)
        {
            this.userName = userName;
            this.password = password;
        }

        public ISession Logon(string rawPassword)
        {
            if (password.Is(rawPassword))
                return new Session(SessionToken.Any(), new Sender(userName));
            return new InvalidSession();
        }

        private sealed class InvalidSession : ISession
        {
            public bool IsValid => false;
        }
    }
}