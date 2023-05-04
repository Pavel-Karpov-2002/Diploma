using System.Collections.Generic;
using UnityEngine;

public class OperationWithItems : MonoBehaviour
{
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private ItemSlot droppedItemButton;
    [SerializeField] private ShowItemInformation showItemInformation;
    [SerializeField] private GameObject itemsPlayerPanel;
    
    private List<ItemSlot> itemsInventory;
    private Item activeItem;
    private Item droppedItem;
    private ItemParameters itemParameters;

    public Item ActiveItem { get => activeItem; set => activeItem = value; }
    public Item DroppedItem => droppedItem;

    public static OperationWithItems instance;

    private void Start()
    {
        itemParameters = gameParameters.ItemStatusWindow;
        itemsInventory = new List<ItemSlot>();
        GetPlayerItems();
    }

    private void GetPlayerItems()
    {
        for (int i = 0; i < itemParameters.CountItems; i++)
        {
            ItemSlot item = Instantiate(itemParameters.ItemSlot);
            item.transform.SetParent(itemsPlayerPanel.transform);
            item.transform.localScale = itemParameters.ItemSlot.transform.localScale;
            item.transform.position = itemParameters.ItemSlot.transform.position;
            itemsInventory.Add(item);
            if (GameSaveParameters.PlayerItems.Count <= i)
            {
                item.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(null));
                continue;
            }

            Item playerItem = GameSaveParameters.PlayerItems[i];
            item.ItemImage.sprite = playerItem.ItemSprite;
            item.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(playerItem));
        }
    }

    public void GetRandomItem()
    {
        droppedItem = GetRandomItem(gameParameters.Items);
        droppedItemButton.ItemImage.sprite = DroppedItem.ItemSprite;
        droppedItemButton.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(DroppedItem));
    }

    public void DeleteItem()
    {
        int activeItem = GameSaveParameters.PlayerItems.FindIndex(item => item == ActiveItem);
        if (activeItem == -1)
            return;
        GameSaveParameters.PlayerItems.RemoveAt(activeItem);
        itemsInventory[activeItem].ItemImage.sprite = itemParameters.ItemSlot.ClearItemImage;
        itemsInventory[activeItem].ItemButton.onClick.RemoveAllListeners();
    }

    public void ChangePlayerItem(Item oldItem, Item newItem)
    {
        if (newItem == null)
            return;

        int changingItem = GameSaveParameters.PlayerItems.FindIndex(x => x == oldItem);
        if (changingItem != -1)
            GameSaveParameters.PlayerItems[changingItem] = newItem;
        else
        {
            GameSaveParameters.PlayerItems.Add(newItem);
        }

        ChangePlayerItemInInventary(changingItem, newItem);
        ClearDroppedItem();
    }

    private void ChangePlayerItemInInventary(int changingItem, Item newItem)
    {
        if (changingItem == -1)
            changingItem = GameSaveParameters.PlayerItems.Count - 1;
        itemsInventory[changingItem].ItemImage.sprite = newItem.ItemSprite;
        itemsInventory[changingItem].ItemButton.onClick.RemoveAllListeners();
        itemsInventory[changingItem].ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(newItem));
    }

    private void ClearDroppedItem()
    {
        droppedItem = null;
        droppedItemButton.ItemImage.sprite = itemParameters.ItemSlot.ClearItemImage;
        droppedItemButton.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(null));
    }

    private Item GetRandomItem(List<Item> items)
    {
        return items[UnityEngine.Random.Range(0, items.Count - 1)];
    }

    public static OperationWithItems GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<OperationWithItems>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
