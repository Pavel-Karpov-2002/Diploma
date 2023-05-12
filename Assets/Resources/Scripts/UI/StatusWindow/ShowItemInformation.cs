using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowItemInformation : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI textInformation;
    [SerializeField] private TextMeshProUGUI textNameItem;
    [SerializeField] private Image playerSkin;

    private bool isShow;
    private Item lastItem;

    private void Start()
    {
        playerSkin.sprite = PlayerMovement.Instance.gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetIventoryItemInformation(Item item)
    {
        ChangePlayerItem(item);
        if (item == null)
            return;
        itemImage.sprite = ConvertTexture2D.GetSprite(ConvertTexture2D.GetTexture2D(item.ItemSpritePath));
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
                Debug.Log("У вас уже есть этот предмет!");
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
