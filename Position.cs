namespace SlayBot
{
    public class Position
    {
        public double x, y;

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetPosition(double x, double y)
        {
            this.x = x; this.y = y;
        }
    }
}
