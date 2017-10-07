using System;
using System.Linq;
using chat_server.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace chat_server_test.Domain
{
    [TestFixture]
    public class SessionSpec
    {
        [Test]
        public void Can_retrieve_last_message() =>
            new Session(SessionToken.Any(), Senders.Alice())
                .Append(new Message(Senders.Bob(), 0.ToString(), DateTime.Now))
                .Append(new Message(Senders.Bob(), 3.ToString(), DateTime.Now))
                .Last()
                .Should()
                .Match<Message>(m => m.Serialize((timestamp, sender, content) => content) == 3.ToString());

        [Test]
        public void Page_gets_given_number_of_messages() =>
            new Session(SessionToken.Any(), Senders.Alice())
                .Append(new Message(Senders.Bob(), 0.ToString(), DateTime.Now))
                .Append(new Message(Senders.Bob(), 1.ToString(), DateTime.Now))
                .Append(new Message(Senders.Bob(), 2.ToString(), DateTime.Now))
                .Append(new Message(Senders.Bob(), 3.ToString(), DateTime.Now))
                .Page(1, 2)
                .Last()
                .Should()
                .Match<Message>(m => m.Serialize((timestamp, sender, content) => content) == 2.ToString());

        [Test]
        public void Page_starts_at_message_with_given_index() =>
            new Session(SessionToken.Any(), Senders.Alice())
                .Append(new Message(Senders.Bob(), 0.ToString(), DateTime.Now))
                .Append(new Message(Senders.Bob(), 1.ToString(), DateTime.Now))
                .Page(1, 1)
                .Single()
                .Should()
                .Match<Message>(m => m.Serialize((timestamp, sender, content) => content) == 1.ToString());

        [Test]
        public void Sender_with_same_value_are_equal() =>
            new Sender("test").Should().Be(new Sender("test"));

        [Test]
        public void Sessions_with_same_token_are_equal() =>
            new Session(new SessionToken("test"), Senders.Alice())
                .Should()
                .Be(new Session(new SessionToken("test"), Senders.Bob()));

        [Test]
        public void Tokens_with_same_value_are_equal() =>
            new SessionToken("test").Should().Be(new SessionToken("test"));
    }

    public static class Senders
    {
        public static Sender Bob() => New("Bob");

        public static Sender Alice() => New("Alice");

        public static Sender New(string name) => new Sender(name);
    }
}