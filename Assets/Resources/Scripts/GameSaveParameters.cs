using System.Collections.Generic;

public static class GameSaveParameters
{
    public static List<Item> PlayerItems = new List<Item>();
    private static int occupiedFloor;
    private static int recordFloor;

    public static int OccupiedFloor
    {
        get => occupiedFloor;
        set
        {
            occupiedFloor = value;
            if (occupiedFloor < RecordFloor)
            {
                RecordFloor = occupiedFloor;
            }
        }
    }

    public static int RecordFloor { get => recordFloor; private set => recordFloor = value; }
}
