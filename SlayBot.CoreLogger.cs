using WebSocketSharp;

namespace SlayBot
{
    public partial class Bot
    {
        partial void Core_OnMessageEvent(object? sender, MessageEventArgs e)
        {
            List<string> data = e.Data.Split("$").ToList();
            string packet = data[0];
            data.Remove(packet);

            PacketHandle.Handle(packet, this, data.ToArray());
        }
    }
}