using Common.MessageHandlers;
using Common.Messages.Client;
using SocketServer;

MessageConfig msgConf = new MessageConfig();
msgConf.Initialize();
MessageHandle.Initialize(msgConf);

ServerMessageHandler serverHandler = new();

MessageHandle.RegisterMessageHandler<ClientRequestNumMessage>(serverHandler.SendNumRespondToClient<ClientRequestNumMessage>);
MessageHandle.RegisterMessageHandler<ClientResponseFactorialMessage>(serverHandler.ConsumeFactorial<ClientResponseFactorialMessage>);

Server server = new Server(5000);
server.Start();
Console.WriteLine("\n Press any key to continue...");
Console.ReadKey();