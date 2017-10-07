using System;

namespace chat_server.Domain
{
    public struct SessionToken
    {
        public SessionToken(string value) => Value = value;

        public string Value { get; }

        public static SessionToken Any() => new SessionToken(Guid.NewGuid().ToString());
    }
}