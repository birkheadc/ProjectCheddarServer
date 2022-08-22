namespace ProjectCheddarServer;

public class ServerPacketSender
{
    private readonly Server server;
    public ServerPacketSender(Server server)
    {
        this.server = server;
    }
    public void Welcome(Guid clientId, string msg)
    {
        using (Packet packet = new((int)ServerPackets.welcome))
        {
            packet.Write(msg);
            packet.Write(clientId.ToString());

            SendTCPData(clientId, packet);
        }
    }

    private void SendTCPData(Guid clientId, Packet packet)
    {
        packet.WriteLength();
        server.SendDataToClient(clientId, packet);
    }
}