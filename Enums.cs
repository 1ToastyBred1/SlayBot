namespace SlayBot
{
    public enum Direction : sbyte
    {
        South       = 0,
        SouthWest   = 1,
        West        = 2,
        NorthWest   = 3,
        North       = 4,
        NorthEast   = 5,
        East        = 6,
        SouthEast   = 7
    }

    public enum MoveCommand : sbyte
    {
        Up    = 1,
        Down  = 2,
        Left  = 3,
        Right = 4
    }
}
