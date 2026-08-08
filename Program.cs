using SlayBot;
using WebSocketSharp;

public class Program 
{
    public static void Main()
    {
        Bot b = new Bot("eu");

        b.AutoRespawn();
        b.JoinGame(1889);
        b.JoinTeam(1);
        AntiAFK(b);

        Console.ReadLine();

        //bot.logger.SaveLogs(); // Optional
    }

    static async void AntiAFK(Bot b)
    {
        await Task.Run(() => 
        { 
            while (true)
            {
                b.Send("dirU$-1");
                Thread.Sleep(1000);
                b.ChangeDirection(Direction.West);
            }
        });
    }
}