using Common.Buffer;
using Common.Messages;
using System.Net.Sockets;
using System.Text;

namespace Common.MessageHandlers
{
    public class MessageHandle
    {
        public readonly MessageConfig MsgConfig;
        public MessageHandle(MessageConfig messageConfig)
        {
            MsgConfig = messageConfig;
        }

        public string HandleMessage(byte[] data, Socket socketToRespond)
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
                    foreach (var handler in MsgConfig.GetHandlers(packetID))
                    {
                        handler.Invoke(message, new ResponseToClient(this, socketToRespond));
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
