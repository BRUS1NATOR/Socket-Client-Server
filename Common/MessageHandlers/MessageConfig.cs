using Common.Buffer;
using Common.Message.Server;
using Common.MessageHandlers.Common;
using Common.MessageHandlers.Handlers.Client;
using Common.MessageHandlers.Handlers.Server;
using Common.Messages;
using Common.Messages.Client;

namespace Common.MessageHandlers
{
    public class MessageConfig
    {
        public delegate void MessageHandler<T>(T message, ResponseToClient socketToRespond) where T : IBaseMessage;

        private IDictionary<MessageIdentifier, MessageHandler<IBaseMessage>> MessageHandlers;

        public MessageConfig()
        {
            MessageHandlers = new Dictionary<MessageIdentifier, MessageHandler<IBaseMessage>>();
        }

        public MessageConfig DefaultHandlers()
        {
            AddMessageHandler<StringMessage>(SimpleStringHandler<StringMessage>.Handle);
            return this;
        }

        public MessageConfig ServerHandlers()
        {
            //Запрос клиента на сервер с просьбой получить число
            AddMessageHandler<ClientRequestNumMessage>(ServerReponseNumHandler<ClientRequestNumMessage>.Handle);
            //Ответ клиента с факториалом
            AddMessageHandler<ClientResponseFactorialMessage>(ServerReceiveFactorialHandler<ClientResponseFactorialMessage>.Handle);
            return this;
        }

        public MessageConfig ClientHandlers()
        {
            //Ответ от сервера с числом
            AddMessageHandler<ServerResponseNumMessage>(ClientFactorialHandler<ServerResponseNumMessage>.Handle);
            return this;
        }

        public void AddMessageHandler<T>(MessageHandler<IBaseMessage> handler) where T : IBaseMessage
        {
            MessageHandlers.Add(new MessageIdentifier(typeof(T), MessageHandlers.Count() + 1), handler);
        }

        public IEnumerable<MessageHandler<IBaseMessage>> GetHandlers(int id)
        {
            return MessageHandlers.Where(x => x.Key.MessageId == id).Select(x=>x.Value);
        }
        public IEnumerable<MessageHandler<IBaseMessage>> GetHandlers(Type type)
        {
            return MessageHandlers.Where(x => x.Key.MessageType.Equals(type)).Select(x => x.Value);
        }

        public int GetMessageId(Type messageType)
        {
            if(!MessageHandlers.Any(x => x.Key.MessageType.Equals(messageType)))
            {
                throw new Exception($"No id specified for type {messageType} create it with 'AddMessageHandler' in MessageConfig");
            }

            return MessageHandlers.Where(x => x.Key.MessageType.Equals(messageType)).FirstOrDefault().Key.MessageId;
        }

        public int GetMessageId(IBaseMessage message)
        {
            return GetMessageId(message.GetType());
        }

        public BaseMessage GetMessage(int messageTypeId, ByteBuffer buffer)
        {
            var a = MessageHandlers.Keys.Where(x => x.MessageId == messageTypeId).FirstOrDefault();
            if (a == null || a.MessageType == null)
            {
                return null;
            }
            
            return (BaseMessage)Activator.CreateInstance(a.MessageType, buffer);
        }

        //public static Type GetMessageType(int messageTypeId)
        //{
        //    if (!Instance.Messages.Keys.Contains(messageTypeId))
        //    {
        //        return null;
        //    }
        //    return Instance.Messages[messageTypeId];
        //}
    }
}
