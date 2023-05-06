using System.Collections.Generic;

public static class GameSaveParameters
{
    public static List<Item> PlayerItems = new List<Item>();
    private static int recordFloor;

    public static int RecordFloor { get => recordFloor; private set => recordFloor = value; }
}
