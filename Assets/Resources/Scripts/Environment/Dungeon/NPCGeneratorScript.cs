using System.Collections.Generic;
using UnityEngine;

public class NPCGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject NPC;
    [SerializeField] private GameObject layerForNPC;

    public void Generator(MazeParameters parameters, List<Rect> rooms, Vector2 gridScale)
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

    private void SetNPC(MazeParameters parameters, List<Rect> rooms, Vector2 gridScale)
    {
        int countNPC = 0;

        while (true)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].width > parameters.MaxRoomColumns || rooms[i].height > parameters.MaxRoomColumns)
                {
                    if (Random.Range(0, rooms.Count) > rooms.Count / 2)
                    {
                        float x = rooms[i].x + rooms[i].width / 2;
                        float y = rooms[i].y + rooms[i].height / 2;
                        GameObject npc = Instantiate(NPC,
                            new Vector3(
                                (x + (Random.Range(0, 1.5f) * (x < parameters.Width / 2 ? 1 : -1))) * gridScale.x,
                                (y + (Random.Range(0, 1.5f) * (y < parameters.Height / 2 ? 1 : -1))) * gridScale.y,
                                0),
                            NPC.transform.rotation);
                        npc.transform.SetParent(layerForNPC.transform);
                        rooms.Remove(rooms[i]);
                        countNPC++;
                        if (countNPC >= parameters.MinNPCCountOnFloor)
                            return;
                    }
                }
            }
        }
    }
}
