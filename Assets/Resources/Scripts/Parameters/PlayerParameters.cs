using UnityEngine;

[CreateAssetMenu(fileName = "PlayerParameters", menuName = "CustomParameters/PlayerParameters")]
public class PlayerParameters : ScriptableObject
{
    [SerializeField] private float playerSpeed;
    [SerializeField][Min(0)] private float npcTriggerDistance;
    [SerializeField] private LayerMask npcLayer;
    [SerializeField] private LayerMask storyLayer;
    [SerializeField] private LayerMask changeSkinLayer;
    [SerializeField] private ItemParameters itemStatusWindow;
    [SerializeField] private float distanceForActivateStoryWindow;
    [SerializeField] private float distanceForActivateSkinChangeWindow;
    [SerializeField] private int costRollingItems;

    public float PlayerSpeed => playerSpeed;
    public float NPCTriggerDistance => npcTriggerDistance;
    public LayerMask NPCLayer => npcLayer;
    public LayerMask StoryLayer => storyLayer;
    public LayerMask ChangeSkinLayer => changeSkinLayer;
    public ItemParameters ItemStatusWindow => itemStatusWindow;
    public int CostRollingItems => costRollingItems;
    public float DistanceForActivateStoryWindow => distanceForActivateStoryWindow;
    public float DistanceForActivateSkinChangeWindow => distanceForActivateSkinChangeWindow;

}
