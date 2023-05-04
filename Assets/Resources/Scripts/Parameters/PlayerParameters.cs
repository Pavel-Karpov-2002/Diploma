using System;
using UnityEngine;

[Serializable]
public class PlayerParameters
{
    [SerializeField] private float playerSpeed;
    [SerializeField][Min(0)] private float npcTriggerDistance;
    [SerializeField] private KeyCode activateKey;
    [SerializeField] private LayerMask npcLayer;
    [SerializeField][Min(0)] private int minPointsForCorrectAnswer;
    [SerializeField][Min(0)] private int minPointsForWrongAnswer;

    public float PlayerSpeed => playerSpeed;
    public float NPCTriggerDistance => npcTriggerDistance;
    public KeyCode ActivateKey => activateKey;
    public LayerMask NPCLayer => npcLayer;
    public int MinPointsForCorrectAnswer => minPointsForCorrectAnswer;
    public int MinPointsForWrongAnswer => minPointsForWrongAnswer;

}
