using Common;
using Common.Message.Server;
using Common.Messages;
using Common.Messages.Client;

namespace SocketServer
{
    public class ServerMessageHandler
    {
        static string dbName => "db.txt";

        public void SendNumRespondToClient<T>(IBaseMessage message, ResponseToMachine toRespond) where T : ClientRequestNumMessage
        {
            if (!File.Exists(dbName))
            {
                File.Create(dbName).Close();
            }

            TextReader tw = new StreamReader(dbName, true);
            var line = tw.ReadLine();
            int num = 1;
            while (line != null)
            {
                num = Convert.ToInt32(line.Split('=')[0]);
                line = tw.ReadLine();
            }
            tw.Close();
            num++;
            toRespond.Send(new ServerResponseNumMessage() { Num = num });
        }

        public void ConsumeFactorial<T>(IBaseMessage message, ResponseToMachine toRespond) where T : ClientResponseFactorialMessage
        {
            var result = (T)message;
            if (!File.Exists(dbName))
            {
                File.Create(dbName);
                Thread.Sleep(1000);
            }

            TextWriter tw = new StreamWriter(dbName, true);
            tw.WriteLine($"{result.Num} = {result.Factorial}");
            tw.Close();


            Console.WriteLine($"Факториал для {result.Num} = {result.Factorial}");
        }

        public static void HandleString<T>(T message, ResponseToMachine toRespond) where T : StringMessage
        {
            Console.WriteLine(message.Message);
        }
    }
}
