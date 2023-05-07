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
        playerSkin.sprite = PlayerMovement.GetInstance().GetComponent<SpriteRenderer>().sprite;
    }

    public void SetIventoryItemInformation(Item item)
    {
        ChangePlayerItem(item);
        if (item == null)
            return;
        itemImage.sprite = item.ItemSprite;
        textNameItem.text = item.Skill.SkillName;
        textInformation.text = item.Skill.SkillInformation;
        OperationWithItems.GetInstance().ActiveItem = item;
    }

    private void ChangePlayerItem(Item item)
    {
        Item newItem = OperationWithItems.GetInstance().DroppedItem;

        if (newItem == null || newItem == item)
        {
            isShow = false;
            return;
        }

        foreach (var playerItem in GameSaveParameters.PlayerItems)
        {
            if (playerItem.Skill.GetType() == newItem.Skill.GetType())
            {
                Debug.Log("У вас уже есть этот предмет!");
                return;
            }
        }

        if (item == null && !isShow)
        {
            isShow = true;
            return;
        }

        if (isShow)
        {
            OperationWithItems.GetInstance().ChangePlayerItem(item, newItem);
            isShow = false;
        }

        if (lastItem != item)
        {
            lastItem = item;
            isShow = true;
            return;
        }
    }
}
