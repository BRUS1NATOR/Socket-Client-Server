namespace Common.MessageHandlers
{
    public class MessageIdentifier
    {
        public Type MessageType { get; set; }
        public int MessageId { get; set; }

        public MessageIdentifier(Type message, int messageId)
        {
            MessageType = message;
            MessageId = messageId;
        }
    }
}
