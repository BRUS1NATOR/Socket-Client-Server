using Common.MessageHandlers;
using SocketServer;

MessageConfig msgConf = new MessageConfig();
msgConf.DefaultHandlers().ServerHandlers().ClientHandlers();
MessageHandle msgConfig = new MessageHandle(msgConf);

Server server = new Server(5000, msgConfig);
server.Start();
Console.WriteLine("\n Press any key to continue...");
Console.ReadKey();