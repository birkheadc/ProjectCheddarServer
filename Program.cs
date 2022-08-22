using ProjectCheddarServer.Constants;

namespace ProjectCheddarServer;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Starting server...");

        Server server = new(NetworkConstants.MAX_PLAYERS, NetworkConstants.PORT);
        server.Start();
    }
}