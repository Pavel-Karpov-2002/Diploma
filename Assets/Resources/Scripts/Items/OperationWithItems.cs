using System.Collections.Generic;
using UnityEngine;

public class OperationWithItems : Singleton<OperationWithItems>
{
    [SerializeField] private ItemParameters itemParameters;
    [SerializeField] private GameParameters gameParameters;
    [SerializeField] private ItemSlot droppedItemButton;
    [SerializeField] private ShowItemInformation showItemInformation;
    [SerializeField] private GameObject itemsPlayerPanel;
    
    private List<ItemSlot> itemsInventory;
    private Item activeItem;
    private Item droppedItem;

    public Item ActiveItem { get => activeItem; set => activeItem = value; }
    public Item DroppedItem => droppedItem;

    private void Start()
    {
        GetPlayerItems();
    }

    private void GetPlayerItems()
    {
        itemsInventory = new List<ItemSlot>();
        for (int i = 0; i < itemParameters.CountItems; i++)
        {
            ItemSlot item = Instantiate(itemParameters.ItemSlot);
            item.transform.SetParent(itemsPlayerPanel.transform);
            item.transform.localScale = itemParameters.ItemSlot.transform.localScale;
            item.transform.position = itemParameters.ItemSlot.transform.position;
            itemsInventory.Add(item);
            if (GameData.Data.PlayerItems.Count <= i)
            {
                item.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(null));
                continue;
            }
            Item playerItem = GameData.Data.PlayerItems[i];
            item.ItemImage.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(playerItem.ItemSpritePath));
            item.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(playerItem));
        }
    }

    public void GetRandomItem()
    {
        droppedItem = GetRandomItem(gameParameters.Items);
        droppedItemButton.ItemButton.onClick.RemoveAllListeners();
        droppedItemButton.ItemImage.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(DroppedItem.ItemSpritePath));
        droppedItemButton.ItemButton.onClick.AddListener(() => showItemInformation.SetIventoryItemInformation(DroppedItem));
    }

    public void DeleteItem()
    {
        int activeItem = GameData.Data.PlayerItems.FindIndex(item => item == ActiveItem);
        if (activeItem == -1)
            return;
        GameData.Data.PlayerItems.RemoveAt(activeItem);
        ChangePanelScript.ClearPanel(itemsPlayerPanel);
        GetPlayerItems();
    }

    public void ChangePlayerItem(Item oldItem, Item newItem)
    {
        if (newItem == null)
            return;
        int changingItem = GameData.Data.PlayerItems.FindIndex(x => x == oldItem);
        if (changingItem != -1)
            GameData.Data.PlayerItems[changingItem] = newItem;
        else
            GameData.Data.PlayerItems.Add(newItem);

        GameData.UpdateGameDataFile(gameParameters.DataPath);
        ChangePlayerItemInInventary(changingItem, newItem);
        ClearDroppedItem();
    }

    private void ChangePlayerItemInInventary(int changingItem, Item newItem)
    {
        if (changingItem == -1)
            changingItem = GameData.Data.PlayerItems.Count - 1;
        itemsInventory[changingItem].ItemImage.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(newItem.ItemSpritePath));
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
        return items[UnityEngine.Random.Range(0, items.Count)];
    }
}
