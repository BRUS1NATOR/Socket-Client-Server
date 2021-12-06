using Common.Buffer;
using Common.Message.Server;
using Common.Messages;
using Common.Messages.Client;

namespace Common.MessageHandlers
{
    public class MessageConfig
    {
        public IList<MessageIdentifier> Messages;

        public MessageConfig()
        {
            Messages = new List<MessageIdentifier>();
        }

        public MessageConfig Initialize()
        {
            DefaultMessages().ServerMessages().ClientMessages();

            return this;
        }

        public void AddMessage(Type message)
        {
            Messages.Add(new MessageIdentifier(message, Messages.Count + 1));
        }

        public void AddMessage(int messageId, Type message)
        {
            if(Messages.Select(x => x.MessageId).Contains(messageId))
            {
                throw new Exception("List already contains this ID");
            }
            Messages.Add(new MessageIdentifier(message, messageId));
        }

        private MessageConfig DefaultMessages()
        {
            AddMessage(typeof(StringMessage));
            return this;
        }

        public MessageConfig ClientMessages()
        {
            AddMessage(typeof(ClientRequestNumMessage));
            AddMessage(typeof(ClientResponseFactorialMessage));
            return this;
        }

        public MessageConfig ServerMessages()
        {
            AddMessage(typeof(ServerResponseNumMessage));
            return this;
        }

        public int GetMessageId(Type messageType)
        {
            if(!Messages.Any(x => x.MessageType.Equals(messageType)))
            {
                throw new Exception($"No id specified for type {messageType} create it with 'AddMessageHandler' in MessageConfig");
            }

            return Messages.Where(x => x.MessageType.Equals(messageType)).FirstOrDefault().MessageId;
        }

        public int GetMessageId(IBaseMessage message)
        {
            return GetMessageId(message.GetType());
        }

        public MessageIdentifier GetMessageIdentifier(Type type)
        {
            return Messages.Where(x => x.MessageType.Equals(type)).FirstOrDefault();
        }

        public BaseMessage GetMessage(int messageTypeId, ByteBuffer buffer)
        {
            var a = Messages.Where(x => x.MessageId == messageTypeId).FirstOrDefault();
            if (a == null || a.MessageType == null)
            {
                return null;
            }
            
            return (BaseMessage)Activator.CreateInstance(a.MessageType, buffer);
        }
    }
}
