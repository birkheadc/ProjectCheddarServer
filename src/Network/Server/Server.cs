using System.Net.Sockets;
using System.Net;
using ProjectCheddarServer.Constants;

namespace ProjectCheddarServer;

public class Server
{
    private bool isRunning = false;
    private Thread mainThread;
    public int MaxPlayers { get; private set; }
    public int Port { get; private set; }
    public Dictionary<Guid, Client> Clients = new();
    private TcpListener tcpListener;
    private ServerPacketSender packetSender;
    private ServerPacketHandler packetHandler;
    private ThreadManager threadManager;
    private GameLogic gameLogic;
    public delegate void PacketHandler(Guid clientId, Packet packet);
    public Dictionary<int, PacketHandler> PacketHandlers;

    public Server(int maxPlayers, int port)
    {
        MaxPlayers = maxPlayers;
        Port = port;

        tcpListener = new TcpListener(IPAddress.Any, Port);

        packetSender = new(this);
        packetHandler = new(this);
        threadManager = new();
        gameLogic = new(threadManager);

        mainThread = new(new ThreadStart(MainThread));

        InitializeServerData();
    }

    private void MainThread()
    {
        Console.WriteLine("Main thread started, running at {0} ticks per second.", ServerConstants.TICKS_PER_SECOND);
        DateTime nextLoop = DateTime.UtcNow;

        while (isRunning)
        {
            while (nextLoop < DateTime.UtcNow)
            {
                gameLogic.Update();
                nextLoop = nextLoop.AddMilliseconds(ServerConstants.MS_PER_TICK);
                if (nextLoop > DateTime.UtcNow) Thread.Sleep(nextLoop - DateTime.UtcNow);
            }
        }
    }

    public void Start()
    {
        isRunning = true;
        Console.WriteLine("Server started.");
        mainThread.Start();
        tcpListener.Start();
        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);
    }

    private void TCPConnectionCallback(IAsyncResult result)
    {
        TcpClient client = tcpListener.EndAcceptTcpClient(result);
        tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectionCallback), null);
        Console.WriteLine("Incoming connection from {0}", client.Client.RemoteEndPoint);

        if (Clients.Count >= MaxPlayers)
        {
            Console.WriteLine("Server is full! {0} was unable to connect.", client.Client.RemoteEndPoint);
            return;
        }

        Guid id = Guid.NewGuid();
        Clients.Add(id, new Client(id, this));
        Clients[id].Tcp.Connect(client);
        packetSender.SendWelcome(id, "Welcome to Project Cheddar!");
        Console.WriteLine("Client connected. Id:{0}", id);
    }
    
    public void SendDataToClient(Guid clientId, Packet packet)
    {
        Clients[clientId].Tcp.SendData(packet);
    }

    public void ExecuteOnMainThread(Action action)
    {
        threadManager.ExecuteOnMainThread(action);
    }

    private void InitializeServerData()
    {
        PacketHandlers = new()
        {
            { (int)ClientPackets.WelcomeReceived, packetHandler.WelcomeReceived }
        };
        Console.WriteLine("Server Data Initialized.");
    }
}