using UnityEngine;

[CreateAssetMenu(fileName = "ItemParameters", menuName = "CustomParameters/ItemParameters")]
public class ItemParameters : ScriptableObject
{
    [SerializeField] private ItemSlot itemSlot;
    [SerializeField][Min(0)] private int countItems;
    [SerializeField] private float distanceActivateItemDrop;
    [SerializeField] private LayerMask layerActivateItemDrop;

    public ItemSlot ItemSlot => itemSlot;
    public int CountItems => countItems;
    public float DistanceActivateItemDrop => distanceActivateItemDrop;
    public LayerMask LayerActivateItemDrop => layerActivateItemDrop;
}
