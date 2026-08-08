namespace SlayBot
{
    public class Entity
    {
        public bool isSelf;

        public string nickname;
        public int lobbyID;

        public Position pos;

        public Entity(string nickname, int lobbyID, Position pos)
        {
            this.nickname = nickname;
            this.lobbyID = lobbyID;
            this.pos = pos;
            isSelf = false;
        }

        public static Entity? FindEntity(Bot b, int id)
        {
            foreach(Entity e in b.Entities)
            {
                if (e.lobbyID == id)
                {
                    return e;
                }
            }

            return null;
        }
    }
}
