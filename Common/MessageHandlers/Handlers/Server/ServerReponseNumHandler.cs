using Common.Message.Server;
using Common.Messages;
using Common.Messages.Client;

namespace Common.MessageHandlers.Handlers.Server
{
    public class ServerReponseNumHandler<T> : MessageHandlerBase<T> where T : ClientRequestNumMessage
    {
        static string dbName => "db.txt";

        public static void Handle(IBaseMessage message, ResponseToClient toRespond)
        {
            CheckType(message);
            HandleTargetType((T)message, toRespond);
        }

        public static void HandleTargetType(T message, ResponseToClient toRespond)
        {
            if (!File.Exists(dbName))
            {
                File.Create(dbName).Close();
            }

            TextReader tw = new StreamReader(dbName, true);
            var line = tw.ReadLine();
            int num = 1;
            while (line!=null)
            {
                num = Convert.ToInt32(line.Split('=')[0]);
                line = tw.ReadLine();
            }
            tw.Close();
            num++;
            toRespond.Send(new ServerResponseNumMessage() { Num = num });
        }
    }
}
