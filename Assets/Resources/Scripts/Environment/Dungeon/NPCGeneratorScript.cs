using System.Collections.Generic;
using UnityEngine;

public class NPCGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject student;
    [SerializeField] private GameObject teacher;
    [SerializeField] private GameObject layerForNPC;

    public void Generator(GameParameters parameters, List<Rect> rooms, Vector2 gridScale)
    {
        ClearNPC();
        SetNPC(parameters, rooms, gridScale);
    }

    private void ClearNPC()
    { 
        foreach (var npc in layerForNPC.GetComponentsInChildren<NPC>())
        {
            if (npc.gameObject != layerForNPC)
            {
                Destroy(npc.gameObject);
            }
        }
    }

    private void SetNPC(GameParameters parameters, List<Rect> rooms, Vector2 gridScale)
    {
        int countNPC = 0;
        List<Rect> saveRooms = new List<Rect>(rooms);

        while (true)
        {
            for (int i = 0; i < saveRooms.Count; i++)
            {
                if (saveRooms[i].width > parameters.Maze.MaxRoomColumns || saveRooms[i].height > parameters.Maze.MaxRoomColumns)
                {
                    if (Random.Range(0, saveRooms.Count) > saveRooms.Count / 2)
                    {
                        float x = saveRooms[i].x + saveRooms[i].width / 2;
                        float y = saveRooms[i].y + saveRooms[i].height / 2;

                        GameObject npc = SetNPC(
                            new Vector3(
                                (x + (Random.Range(0, 1.5f) * (x < parameters.Maze.Width / 2 ? 1 : -1))) * gridScale.x,
                                (y + (Random.Range(0, 1.5f) * (y < parameters.Maze.Height / 2 ? 1 : -1))) * gridScale.y,
                                0), 
                            (countNPC == 0 ? teacher : student));

                        npc.transform.SetParent(layerForNPC.transform);

                        saveRooms.Remove(saveRooms[i]);
                        countNPC++;
                        if (countNPC >= parameters.Maze.MinNPCCountOnFloor + (GameSaveParameters.OccupiedFloor / parameters.NPC.AdditionalNPCTroughtFloor))
                            return;
                    }
                }
            }
        }
    }

    private GameObject SetNPC(Vector3 position, GameObject npc)
    {
        return Instantiate(npc, position, npc.transform.rotation);
    }
}
