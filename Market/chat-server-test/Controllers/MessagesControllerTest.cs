using System.Linq;
using chat_server.Controllers;
using chat_server.Domain;
using chat_server.Persistence;
using chat_server_test.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace chat_server_test.Controllers
{
    [TestFixture]
    public class MessagesControllerTest
    {
        [Test]
        public void Sender_session_gets_messages_once()
        {
            var token = SessionToken.Any();
            var repository = new InMemorySessions();
            repository.Save(new Session(token, Senders.Bob()));
            var cut = new MessagesController(repository);
            cut.Post(token.Value, "Hi");
            repository.Load(token).Page(0, 10).Count().Should().Be(1);
        }
    }
}