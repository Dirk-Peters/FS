using System.Collections.Generic;
using chat_server.Domain;

namespace chat_server.Persistence
{
    public interface ISessionRepository
    {
        void Delete(SessionToken token);
        Session Load(SessionToken token);
        Session Create(SessionToken token, Sender owner);
        Session Save(Session session);
        IEnumerable<Session> All();
    }
}