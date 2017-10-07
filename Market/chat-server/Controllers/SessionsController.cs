using chat_server.Domain;
using chat_server.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace chat_server.Controllers
{
    [Route(Api.Name + "/sessions")]
    public class SessionsController : Controller
    {
        private readonly ISessionRepository sessions;

        public SessionsController(ISessionRepository sessions) => this.sessions = sessions;

        [HttpPost]
        public string Post(string userName) =>
            sessions.Create(SessionToken.Any(), new Sender(userName)).Token.Value;
    }
}