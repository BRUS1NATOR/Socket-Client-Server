
using Common.Messages;
using Common.Messages.Client;

namespace Common.MessageHandlers.Handlers.Server
{
    public class ServerReceiveFactorialHandler<T> : MessageHandlerBase<T> where T : ClientResponseFactorialMessage
    {
        public static string dbName => "db.txt";
        public static void Handle(IBaseMessage message, ResponseToClient toRespond)
        {
            CheckType(message);
            HandleTargetType((T)message, toRespond);
        }

        public static void HandleTargetType(T message, ResponseToClient toRespond)
        {
            if (!File.Exists(dbName))
            {
                File.Create(dbName);
                Thread.Sleep(1000);
            }

            TextWriter tw = new StreamWriter(dbName, true);
            tw.WriteLine($"{message.Num} = {message.Factorial}");
            tw.Close();


            Console.WriteLine($"Факториал для {message.Num} = {message.Factorial}");
        }
    }
}
