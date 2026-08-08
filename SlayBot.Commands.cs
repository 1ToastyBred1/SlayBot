using WebSocketSharp;

namespace SlayBot
{
    public partial class Bot
    {
        public void AutoRespawn()
        {
            autorespawn = !autorespawn;

            if (autorespawn)
            {
                logger.OnMessageEvent += CheckIfRespawn;
                return;
            }
            logger.OnMessageEvent -= CheckIfRespawn;

            void CheckIfRespawn(object? sender, MessageEventArgs e)
            {
                if (e.Data.StartsWith("hp$") && e.Data.Split("$")[2] == "0")
                {
                    Respawn();
                }
            }
        }
        public void Login(Account acc)
        {
            Send($"login${acc.name}${acc.password}");
        }

        public void Register(Account acc)
        {
            Send($"register${acc.name}${acc.password}${acc.email}");
        }

        public void Logout() { Send("Logout"); }

        public void MessageChat(string message)
        {
            Send($"chat{message}");
        }

        public void SwitchWeapon(int weapon)
        {
            Send($"sW${weapon}");
        }

        public void JoinGame(int game)
        {
            Send($"join-game${game}");
        }

        public void JoinRandomGame(int mode, bool anonym = false)
        {
            name = anonym ? string.Empty : name;
            Send($"joinRandomGame${name}${mode}");
        }

        public void LeaveGame()
        {
            Send("leave-game");
        }

        public void CreateGame(int map, int time, int bots, int gamemode, bool public_room = true)
        {
            Send($"create-game${map}${time}${bots}${gamemode}${Convert.ToByte(!public_room)}");
        }

        public void JoinTeam(int team)
        {
            Send($"joinTeam${team}");
        }

        public void Shoot(double x, double y)
        {
            Send($"md${x}${y}");
        }

        public void StopShooting()
        {
            Send("mu");
        }

        public void ChangeDirection(Direction dir)
        {
            Send($"dirU${(sbyte)dir}");
        }

        public void MoveCommand(MoveCommand dir, bool stop = false)
        {
            string cmd = stop ? "ku" : "kd";
            Send($"{cmd}${dir}");
        }

        public void Respawn()
        {
            Send("respawn");
        }

        public void MapVote(int map)
        {
            Send($"map-vote${map}");
        }

        public void UseAbility(int ab, double x = 0, double y = 0)
        {
            Send($"ab${ab}${x}${y}");
        }
    }
}
