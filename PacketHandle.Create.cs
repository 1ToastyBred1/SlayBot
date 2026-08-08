namespace SlayBot
{
    public abstract partial class PacketHandle
    {
        /* TODO: Add comments? */
        private static Dictionary<string, PacketHandle> Create()
        {
            Dictionary<string, PacketHandle> result = new();

            List<PacketHandle> exporters = typeof(PacketHandle)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(PacketHandle)) && !t.IsAbstract)
                .Select(t => (PacketHandle)Activator.CreateInstance(t)!)!.ToList();

            foreach(PacketHandle ph in exporters)
            {
                result.Add(ph.Packet, ph);
            }

            return result;
        }
    }
}
