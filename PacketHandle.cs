namespace SlayBot
{
    public abstract partial class PacketHandle
    {
        public static Dictionary<string, PacketHandle> packethandles = Create();
        public abstract string Packet { get; }

        public abstract void Handle(Bot b, string[] args);
        public static void Handle(string packet, Bot b, string[] args) 
        {
            if (!packethandles.ContainsKey(packet)) { return; }
            packethandles[packet].Handle(b, args);
        }
    }

    public class upd_Handle : PacketHandle
    {
        public override string Packet { get { return "upd"; } }

        public override void Handle(Bot b, string[] args)
        {
            if (args.Length < 2) { return; }

            int entityID;

            bool success = int.TryParse(args[1], out entityID);
            if (!success) { return; }


            double minX = double.Parse(args[2]);
            double minY = double.Parse(args[3]);

            double maxX = double.Parse(args[4]);
            double maxY = double.Parse(args[5]);

            Entity e = Entity.FindEntity(b, entityID)!;
            try { e.pos.SetPosition(minX, minY); }
            catch (Exception) { }
        }
    }

    public class nP_Handle : PacketHandle
    {
        public override string Packet { get { return "nP"; } }

        public override void Handle(Bot b, string[] args)
        {
            int lobbyID = int.Parse(args[0]);

            double x = double.Parse(args[1]);
            double y = double.Parse(args[2]);

            string name = args[3];

            Entity player = new Entity(name, lobbyID, new Position(x, y));

            if (player.lobbyID == b.lobbyID) { player.isSelf = true; }

            b.Entities.Add(player);
        }
    }

    public class pid_Handle : PacketHandle
    {
        public override string Packet { get { return "pid"; } }

        public override void Handle(Bot b, string[] args)
        {
            b.lobbyID = int.Parse(args[0]);
        }
    }

    public class pL_Handle : PacketHandle
    {
        public override string Packet { get { return "pL"; } }

        public override void Handle(Bot b, string[] args)
        {
            Entity? p = Entity.FindEntity(b, int.Parse(args[0]));
            if (p != null)
            {
                b.Entities.Remove(p);
            }
        }
    }
}
