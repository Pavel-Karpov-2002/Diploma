using UnityEngine;

public class FloorInformation : MonoBehaviour
{
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
