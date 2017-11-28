using chat_server.Domain;

namespace chat_server.Domain
{
    public sealed class Password
    {
        private readonly HashedString hashed;

        private Password(HashedString hashed) => this.hashed = hashed;

        public bool Is(string rawPassword) => hashed.Equals(rawPassword);

        public static Password Create(string rawPassword)
            => new Password(HashedString.Hash(rawPassword));
    }
}