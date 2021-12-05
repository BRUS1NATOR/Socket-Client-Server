using Common.Messages;

namespace Common.MessageHandlers.Common
{
    public class SimpleStringHandler<T> : MessageHandlerBase<T> where T : StringMessage
    {
        public static void Handle(IBaseMessage message, ResponseToClient toRespond)
        {
            CheckType(message);
            HandleTargetType((T)message, toRespond);
        }

        public static void HandleTargetType(T message, ResponseToClient toRespond)
        {
            Console.WriteLine(message.Message);
        }
    }
}
