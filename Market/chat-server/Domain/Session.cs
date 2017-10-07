using System;
using System.Collections.Generic;
using System.Linq;

namespace chat_server.Domain
{
    public sealed class Session
    {
        private readonly Message[] messages;
        private readonly Sender owner;

        public Session(SessionToken token, Sender owner)
            : this(token, owner, new Message[0])
        {
        }

        private Session(SessionToken token, Sender owner, Message[] messages)
        {
            Token = token;
            this.owner = owner;
            this.messages = messages;
        }

        public SessionToken Token { get; }

        public Session Append(Message message) => new Session(Token, owner, messages.Concat(new[] {message}).ToArray());

        public IEnumerable<Message> Page(int start, int count) => messages.Skip(start).Take(count);

        public Message Last() => Page(messages.Length - 1, 1).SingleOrDefault();

        public IEnumerable<Session> SendToAll(string content, IEnumerable<Session> recipients)
        {
            var message = new Message(owner, content, DateTime.Now);
            return recipients.Select(r => r.Append(message)).Concat(new[] {Append(message)});
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj is Session session && Equals(session);
        }

        private bool Equals(Session other) => Token.Equals(other.Token);

        public override int GetHashCode() => Token.GetHashCode();
    }
}