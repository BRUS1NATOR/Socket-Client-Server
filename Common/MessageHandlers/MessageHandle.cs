using Common.Buffer;
using Common.Messages;
using System.Net.Sockets;
using System.Text;

namespace Common.MessageHandlers
{
    public static class MessageHandle
    {
        public delegate void MessageHandler<T>(T message, ResponseToMachine socketToRespond) where T : IBaseMessage;

        private static IDictionary<MessageIdentifier, MessageHandler<IBaseMessage>> MessageHandlers;

        public static MessageConfig MsgConfig;

        public static void Initialize(MessageConfig messageConfig)
        {
            MessageHandlers = new Dictionary<MessageIdentifier, MessageHandler<IBaseMessage>>();
            MsgConfig = messageConfig;
        }

        public static void RegisterMessageHandler<T>(MessageHandler<IBaseMessage> handler)
        {
            MessageHandlers.Add(MsgConfig.GetMessageIdentifier(typeof(T)), handler);
        }

        public static IEnumerable<MessageHandler<IBaseMessage>> GetHandlers(int id)
        {
            return MessageHandlers.Where(x => x.Key.MessageId == id).Select(x => x.Value);
        }
        public static IEnumerable<MessageHandler<IBaseMessage>> GetHandlers(Type type)
        {
            return MessageHandlers.Where(x => x.Key.MessageType.Equals(type)).Select(x => x.Value);
        }

        public static string HandleMessage(byte[] data, Socket socketToRespond)
        {
            try
            {
                if (data.Length == 0)
                {
                    return "";
                }

                ByteBuffer buffer = new ByteBuffer(data);

                int packetID = buffer.Read<int>(true);
                var message = MsgConfig.GetMessage(packetID, buffer);

                StringBuilder result = new StringBuilder();

                try
                {
                    var handlers = GetHandlers(packetID);
                    if (!handlers.Any())
                    {
                        Console.WriteLine($"No handlers found for {packetID}");
                        socketToRespond.Close();
                    }
                    foreach (var handler in handlers)
                    {
                        handler.Invoke(message, new ResponseToMachine(socketToRespond));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return result.ToString();
            }
            catch (Exception e)
            { throw e; }
        }
    }
}
