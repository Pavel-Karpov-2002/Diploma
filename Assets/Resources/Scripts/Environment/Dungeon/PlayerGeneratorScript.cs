using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneratorScript : MonoBehaviour
{
    public static void SetPlayerPosition(MazeParameters parameters, List<Rect> rooms, Vector2 gridScale)
    {
        while (true)
        {
            foreach (Rect room in rooms)
            {
                if (room.width > parameters.MaxRoomColumns || room.height > parameters.MaxRoomColumns)
                {
                    if (Random.Range(0, rooms.Count) > rooms.Count / 2)
                    {
                        float x = room.x + room.width / 2;
                        float y = room.y + room.height / 2;
                        PlayerConstructor.GetInstance().gameObject.transform.position = new Vector3(x * gridScale.x, y * gridScale.y, 0);
                        return;
                    }
                }
            }
        }
    }
}
