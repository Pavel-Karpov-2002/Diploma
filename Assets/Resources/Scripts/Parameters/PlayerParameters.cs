using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "CustomParameters/PlayerParameters")]
public class PlayerParameters : ScriptableObject
{
    [SerializeField] private float playerSpeed;
    [SerializeField][Min(0)] private float npcTriggerDistance;
    [SerializeField] private LayerMask npcLayer;
    [SerializeField] private ItemParameters itemStatusWindow;
    [SerializeField] private LayerMask storyLayer;
    [SerializeField] private float diastanceForActivateStoryWindow;

    public float PlayerSpeed => playerSpeed;
    public float NPCTriggerDistance => npcTriggerDistance;
    public LayerMask NPCLayer => npcLayer;
    public ItemParameters ItemStatusWindow => itemStatusWindow;
    public LayerMask StoryLayer => storyLayer;
    public float DiastanceForActivateStoryWindow => diastanceForActivateStoryWindow;

}
