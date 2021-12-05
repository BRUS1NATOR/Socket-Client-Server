using Common.Message.Server;
using Common.Messages;
using Common.Messages.Client;

namespace Common.MessageHandlers.Handlers.Client
{
    public class ClientFactorialHandler<T> : MessageHandlerBase<T> where T : ServerResponseNumMessage
    {
        public static void Handle(IBaseMessage message, ResponseToClient toRespond)
        {
            CheckType(message);
            HandleTargetType((T)message, toRespond);
        }

        public static void HandleTargetType(T message, ResponseToClient toRespond)
        {
            Console.WriteLine("Server respond: " + message.Num);
            StaticData.Fact = 1;
            StaticData.Number = message.Num;
            for (int i = 1; i <= StaticData.Number; i++)
            {
                StaticData.Fact = StaticData.Fact * i;
            }

            Console.WriteLine("Factorial: " + StaticData.Fact);

           // toRespond.Send(new ClientResponseFactorialMessage() { Num = number, Factorial = fact });
        }
    }
}
