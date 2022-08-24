namespace ProjectCheddarServer;

public class ServerPacketHandler
{
    private readonly Server server;

    public ServerPacketHandler(Server server)
    {
        this.server = server;
    }

    public void WelcomeReceived(Guid clientId, Packet packet)
    {
        Guid id = Guid.Parse(packet.ReadString());

        Console.WriteLine("{0} connected successfully and is now registered as id {1}", server.Clients[clientId].Tcp.Socket.Client.RemoteEndPoint, clientId);
    }
}