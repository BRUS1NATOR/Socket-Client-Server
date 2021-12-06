using Common;
using Common.Message.Server;
using Common.Messages;

namespace SocketClient
{
    public class ClientMessageHandler
    {
        //Немного хардкода для выполнения задания)

        public static long Fact;
        public static int Number;
        
        public void HandleTargetType<T>(IBaseMessage message, ResponseToMachine toRespond) where T : ServerResponseNumMessage
        {
            var target = (T)message;
            Console.WriteLine("Server respond: " + target.Num);
            Fact = 1;
            Number = target.Num;
            for (int i = 1; i <= Number; i++)
            {
                Fact = Fact * i;
            }

            Console.WriteLine("Factorial: " + Fact);
        }

        public static void HandleString<T>(T message, ResponseToMachine toRespond) where T : StringMessage
        {
            Console.WriteLine(message.Message);
        }
    }
}
