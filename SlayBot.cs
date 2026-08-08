using WebSocketSharp;

namespace SlayBot
{
    public partial class Bot
    {
        public static byte nextBotID = 0;

        public string name;
        public string password;

        public string proxyserver   = "";
        public string proxyusername = "";
        public string proxypassword = "";

        public Position pos;
        public int lobbyID;

        private bool autorespawn = false;

        public WebSocket ws;
        public Pinger pinger;
        public Logger logger;

        /*\ Bots are also on the list \*/
        public List<Entity> Entities = new();

        public Action<string> Send;

        partial void Core_OnMessageEvent(object? sender, MessageEventArgs e);

        public Bot(string server, string proxyserver = "", string proxyusername = "", string proxypassword = "")
        {
            name = $"Bot{nextBotID}";
            password = string.Empty;

            ws = new WebSocket($"wss://{server}.slay.one:62203/");
            ws.SslConfiguration.EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12;

            if (proxyserver != "")
            {
                this.proxyserver = proxyserver; this.proxyusername = proxyusername; this.proxypassword = proxypassword;
                ws.SetProxy(proxyserver, proxyusername, proxypassword);
            }

            ws.Connect();

            Send = ws.Send;

            pinger = new Pinger();
            pinger.Start(this);

            logger = new Logger(this);
            Send += logger.OnSendEvent;
            ws.OnMessage += (sender, e) => { logger.OnMessageEvent(sender, e); };
            logger.OnMessageEvent += Core_OnMessageEvent;

            nextBotID++;

            pos = new Position(0, 0);
        }
    }

    public class Account
    {
        public string name, password, email;

        public Account(string n, string p, string e)
        {
            name = n; password = p; email = e;
        }

        public static Account GenerateRandomAccount() 
        {
            Random rand = new Random();

            string chars = "abcdefghjklmnopqrstuvwxyz";

            string randomName = "";
            string randomPass = "";
            string randomEmail = "";

            for (int i = 0; i < 10; i++) 
            {
                randomName += chars[rand.Next(chars.Length)];
                randomPass += chars[rand.Next(chars.Length)];
                randomEmail += chars[rand.Next(chars.Length)];
            }

            return new Account(randomName, randomPass, randomEmail + "@gmail.com");
        }

        public static void SaveRandomAccounts(string path, int amount)
        {
            string res = "";

            for (int i = 0; i < amount; i++)
            {
                Account a = GenerateRandomAccount();
                res += $"{a.name}${a.password}${a.email}\n";
            }

            File.WriteAllText(path, res.TrimEnd('\n'));
        }

        public static List<Account> ParseAccounts(string path)
        {
            List<Account> accs = new();
            string[] lines = File.ReadAllLines(path);

            foreach(string line in lines)
            {
                string[] acc_info = line.Split("$");
                accs.Add(new Account(acc_info[0], acc_info[1], acc_info[2]));
            }

            return accs;
        }
    }
}
