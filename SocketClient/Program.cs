using Common;
using Common.MessageHandlers;
using Common.MessageHandlers.Handlers.Client;
using Common.Messages;
using Common.Messages.Client;

MessageConfig msgConf = new MessageConfig();
msgConf.DefaultHandlers().ServerHandlers().ClientHandlers();
MessageHandle msgHandler = new MessageHandle(msgConf);

Client client = new Client("localhost", 5000, msgHandler);

ClientRequestNumMessage message = new ClientRequestNumMessage();
message.Message = "Throw me some numbers";

client.Send(message);

//Thread receive = new Thread(() =>
//{
//    while (true)
//    {
//        client.Receive();
//    }
//});

client.Receive();


Console.WriteLine("Shutdown..");
client.Dispose();

ClientResponseFactorialMessage response = new ClientResponseFactorialMessage(){ Num = StaticData.Number, Factorial = StaticData.Fact};
message.Message = "Throw me some numbers";

client = new Client("localhost", 5000, msgHandler);
client.Send(response);


Console.WriteLine("Press any key to quit");
Console.ReadKey();