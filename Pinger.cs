namespace SlayBot
{
    public class Pinger
    {
        public bool ping = true;

        public async void Start(Bot bot)
        {
            await Task.Run(() =>
            {
                while (ping)
                {
                    bot.Send("ping");

                    Thread.Sleep(2500);
                }
            });
        }
    }
}
