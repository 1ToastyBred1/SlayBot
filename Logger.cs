using WebSocketSharp;

namespace SlayBot
{
    public class Logger
    {
        public List<string> logs = new();
        public Action<object?, MessageEventArgs> OnMessageEvent;
        public Action<string> OnSendEvent;

        public Logger(Bot b, bool log = false)
        {
            OnMessageEvent = (sender, e) => 
            {
                if (e.Data.StartsWith("logged$"))
                {
                    b.name = e.Data.Split("$")[1];
                }

                logs.Add($"{b.name} [Server to Client] {e.Data}");

                if (log) Console.WriteLine($"[{b.name}] From server: {e.Data}");
            };

            OnSendEvent = (data) =>
            {
                logs.Add($"{b.name} [Client to Server] {data}");

                if (log) Console.WriteLine($"[{b.name}] To server: {data}");
            };
        }

        /* Saves logs to disk */
        public void SaveLogs()
        {
            string allLogs = string.Empty;

            foreach (string e in logs.ToList())
            {
                allLogs += $"{e}\n";
            }

            File.WriteAllText("logs.txt", allLogs);
        }
    }
}
