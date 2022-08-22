namespace ProjectCheddarServer;

public class GameLogic
{
    private readonly ThreadManager threadManager;

    public GameLogic(ThreadManager threadManager)
    {
        this.threadManager = threadManager;
    }

    public void Update()
    {
        threadManager.UpdateMain();
    }
}