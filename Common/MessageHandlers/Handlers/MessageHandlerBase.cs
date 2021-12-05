using Common.Messages;

namespace Common.MessageHandlers
{
    public abstract class MessageHandlerBase<T> where T : IBaseMessage
    {
        protected T targetType;
        public static bool IsAssignable(IBaseMessage message)
        {
            return message is T;
        }

        public static string CheckType(IBaseMessage message)
        {
            if (!IsAssignable(message))
            {
                throw new Exception("Message type is incorrect");
            }
            return "Not implemented!";
        }
    }
}
