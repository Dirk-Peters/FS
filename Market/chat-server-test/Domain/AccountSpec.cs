using chat_server.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace chat_server_test.Domain
{
    [TestFixture]
    public class AccountSpec
    {
        [Test]
        public void Can_logon_with_right_password()
            => new Account("user-name", Password.Create("swordfish"))
                .Logon("swordfish")
                .IsValid
                .Should().BeTrue();

        [Test]
        public void Cannot_logon_with_wrong_password()
            => new Account("user-name", Password.Create("swordfish"))
                .Logon("longsword")
                .IsValid
                .Should().BeFalse();
    }
}