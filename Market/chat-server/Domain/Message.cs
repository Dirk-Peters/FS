using System;

namespace chat_server.Domain
{
    public sealed class Message
    {
        private readonly string content;
        private readonly Sender sender;
        private readonly DateTime timeStamp;

        public Message(Sender sender, string content, DateTime timeStamp)
        {
            this.sender = sender;
            this.content = content;
            this.timeStamp = timeStamp;
        }

        public TSerialized Serialize<TSerialized>(Func<DateTime, Sender, string, TSerialized> serializer) =>
            serializer(timeStamp, sender, content);
    }
}