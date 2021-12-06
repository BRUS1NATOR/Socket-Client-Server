using Common;
using Common.Message.Server;
using Common.MessageHandlers;
using Common.Messages;
using Common.Messages.Client;
using SocketClient;

MessageConfig msgConf = new MessageConfig();
msgConf.Initialize();
MessageHandle.Initialize(msgConf);

ClientMessageHandler clientHandler = new();

MessageHandle.RegisterMessageHandler<ServerResponseNumMessage>(clientHandler.HandleTargetType<ServerResponseNumMessage>);


Client client = new Client("localhost", 5000);

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

ClientResponseFactorialMessage response = new ClientResponseFactorialMessage(){ 
    Num = ClientMessageHandler.Number, Factorial = ClientMessageHandler.Fact };


client = new Client("localhost", 5000);
client.Send(response);


Console.WriteLine("Press any key to quit");
Console.ReadKey();