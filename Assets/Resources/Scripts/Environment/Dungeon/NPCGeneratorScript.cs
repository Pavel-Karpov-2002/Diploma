using System;
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
        int amountStudents = DialogScript.Instance.NpcQuestions.AmountStudentsOnFloor;
        while (true)
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                float x = UnityEngine.Random.Range(rooms[i].x + 2, rooms[i].x + rooms[i].width - 2);
                float y = UnityEngine.Random.Range(rooms[i].y + 2, rooms[i].y + rooms[i].height - 2);

                GameObject npc = SetNPC(
                    new Vector3(
                        x * gridScale.x,
                        y * gridScale.y,
                        0),
                    (countNPC == amountStudents ? teacher : student));

                npc.transform.SetParent(layerForNPC.transform);

                countNPC++;
                if (countNPC >= amountStudents + 1)
                    return;
            }
        }
    }

    private GameObject SetNPC(Vector3 position, GameObject npc)
    {
        return Instantiate(npc, position, npc.transform.rotation);
    }
}
