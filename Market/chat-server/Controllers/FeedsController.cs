using System.Collections.Generic;
using System.Linq;
using chat_server.Domain;
using chat_server.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace chat_server.Controllers
{
    [Route(Api.Name + "/feeds")]
    public class FeedsController : Controller
    {
        private readonly ISessionRepository sessions;

        public FeedsController(ISessionRepository sessions) => this.sessions = sessions;

        [HttpGet("{session}")]
        public IEnumerable<object> Get(string session, int start, int count) =>
            sessions.Load(new SessionToken(session))?.Page(start, count)
                .Select(m => m.Serialize((timestamp, sender, content) =>
                    new {TimeStamp = timestamp, Sender = sender.Value, Message = content}));
    }
}