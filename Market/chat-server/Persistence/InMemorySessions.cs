using System.Collections.Concurrent;
using System.Collections.Generic;
using chat_server.Domain;

namespace chat_server.Persistence
{
    public sealed class InMemorySessions : ISessionRepository
    {
        private readonly ConcurrentDictionary<SessionToken, Session> sessions =
            new ConcurrentDictionary<SessionToken, Session>();

        public Session Load(SessionToken token) => sessions.GetValueOrDefault(token);

        public Session Create(SessionToken token, Sender owner) =>
            sessions.GetOrAdd(token, t => new Session(token, owner));

        public Session Save(Session session) =>
            sessions.AddOrUpdate(
                session.Token,
                t => session,
                (t, existing) => session);

        public IEnumerable<Session> All() => sessions.Values;
    }
}