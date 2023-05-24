using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInformation : Singleton<ShowItemInformation>
{
    [SerializeField] private ItemParameters itemParameters;
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI textInformation;
    [SerializeField] private TextMeshProUGUI textNameItem;
    [SerializeField] private Image playerSkin;

    private bool isShow;
    private Item lastItem;

    public Image PlayerSkin => playerSkin;

    protected override void Awake()
    {
        base.Awake();
        playerSkin.sprite = PlayerMovement.Instance.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnEnable()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemImage.sprite = itemParameters.ItemSlot.ClearItemImage;
        textNameItem.text = string.Empty;
        textInformation.text = string.Empty;
        if (OperationWithItems.Instance != null)
            OperationWithItems.Instance.ActiveItem = new Item();
    }

    public void SetIventoryItemInformation(Item item)
    {
        ChangePlayerItem(item);
        if (item == null)
            return;
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID             
        string path = Application.persistentDataPath;
#endif
        itemImage.sprite = Resources.Load<Sprite>(item.ItemSpritePath);
        textNameItem.text = item.ItemName;
        textInformation.text = item.ItemInformation;
        OperationWithItems.Instance.ActiveItem = item;
    }

    private void ChangePlayerItem(Item item)
    {
        Item newItem = OperationWithItems.Instance.DroppedItem;
        if (newItem == null || newItem == item)
        {
            isShow = false;
            return;
        }
        foreach (var playerItem in GameData.Data.PlayerItems)
        {
            if (playerItem.Skill.GetType() == newItem.Skill.GetType())
            {
                isShow = false;
                GameException.Instance.ShowError("У вас уже есть этот предмет!");
                return;
            }
        }
        if (item == null)
        {
            OperationWithItems.Instance.ChangePlayerItem(item, newItem);
            isShow = false;
            return;
        }
        if (lastItem != item)
        {
            lastItem = item;
            isShow = true;
            return;
        }
        if (isShow)
        {
            OperationWithItems.Instance.ChangePlayerItem(item, newItem);
            isShow = false;
        }
    }
}
