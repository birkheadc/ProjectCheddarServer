using System.Net.Sockets;

namespace ProjectCheddarServer;

public class Tcp
{
    public TcpClient Socket;
    private readonly Guid id;
    private NetworkStream stream;
    private byte[] receivedBuffer;
    private Packet receiveData;
    private Server server;

    public Tcp(Guid _id, Server server)
    {
        id = _id;
        this.server = server;
    }

    public void Connect(TcpClient _socket)
    {
        Socket = _socket;
        Socket.ReceiveBufferSize = Client.DataBufferSize;
        Socket.SendBufferSize = Client.DataBufferSize;

        stream = Socket.GetStream();
        receiveData = new Packet();
        receivedBuffer = new byte[Client.DataBufferSize];
        
        stream.BeginRead(receivedBuffer, 0, Client.DataBufferSize, ReceiveCallback, null);
    }

    private void ReceiveCallback(IAsyncResult result)
    {
        try
        {
            int byteLength = stream.EndRead(result);
            if (byteLength <= 0)
            {
                return;
            }

            byte[] data = new byte[byteLength];
            Array.Copy(receivedBuffer, data, byteLength);

            receiveData.Reset(HandleData(data));

            stream.BeginRead(receivedBuffer, 0, Client.DataBufferSize, ReceiveCallback, null);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error receiving TCP: {0}", e);
        }
    }

    public void SendData(Packet packet)
    {
        try
        {
            if (Socket is null) return;
            stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null);
        }
        catch
        {
            Console.WriteLine("Error sending data to player: {0} via TCP", id.ToString());
        }
    }

    private bool HandleData(byte[] data)
    {
        int packetLength = 0;

        receiveData.SetBytes(data);

        if (receiveData.UnreadLength() >= 4)
        {
            packetLength =  receiveData.ReadInt();
            if (packetLength < 1)
            {
                return true;
            }
        }
        while (packetLength > 0  && packetLength <= receiveData.UnreadLength())
        {
            byte[] packetBytes = receiveData.ReadBytes(packetLength);
            server.ExecuteOnMainThread(() =>
            {
                using (Packet packet = new(packetBytes))
                {
                    int packetId = packet.ReadInt();
                    server.PacketHandleDelegates[(ClientPacket)packetId](id, packet);
                }
            });

            packetLength = 0;
            if (receiveData.UnreadLength() >= 4)
            {
                packetLength =  receiveData.ReadInt();
                if (packetLength < 1)
                {
                    return true;
                }
            }
        }

        if (packetLength <= 1) return true;
        return false;
    }
}