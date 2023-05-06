using System.Collections.Generic;
using UnityEngine;

public class NPCGeneratorScript : MonoBehaviour
{
    [SerializeField] private GameObject student;
    [SerializeField] private GameObject teacher;
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
        int amountStudents = 5;
        List<Rect> saveRooms = new List<Rect>(rooms);

        while (true)
        {
            for (int i = 0; i < saveRooms.Count; i++)
            {
                float x = saveRooms[i].x + saveRooms[i].width / 2;
                float y = saveRooms[i].y + saveRooms[i].height / 2;

                GameObject npc = SetNPC(
                    new Vector3(
                        (x + (Random.Range(0, 1.5f) * (x < parameters.Width / 2 ? 1 : -1))) * gridScale.x,
                        (y + (Random.Range(0, 1.5f) * (y < parameters.Height / 2 ? 1 : -1))) * gridScale.y,
                        0),
                    (countNPC == amountStudents - 1 ? teacher : student));

                npc.transform.SetParent(layerForNPC.transform);

                saveRooms.Remove(saveRooms[i]);
                countNPC++;
                if (countNPC >= amountStudents)
                    return;
            }
        }
    }

    private GameObject SetNPC(Vector3 position, GameObject npc)
    {
        return Instantiate(npc, position, npc.transform.rotation);
    }
}
