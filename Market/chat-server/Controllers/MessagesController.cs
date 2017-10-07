using System.Linq;
using chat_server.Domain;
using chat_server.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace chat_server.Controllers
{
    [Route(Api.Name + "/messages")]
    public class MessagesController : Controller
    {
        private readonly ISessionRepository sessions;

        public MessagesController(ISessionRepository sessions) => this.sessions = sessions;

        [HttpPut("{session}")]
        public object Put(string session, string message) =>
            sessions.Load(new SessionToken(session))?
                .SendToAll(message, sessions.All())
                .Select(s => sessions.Save(s))
                .Last().Last().Serialize((timestamp, sender, content) =>
                    new {Timestamp = timestamp, Sender = sender.Value, Message = content});
    }
}