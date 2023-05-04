using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(Button))]
public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Button itemButton;
    [SerializeField] private Image itemImage;
    [SerializeField] private Sprite clearItemImage;

    public Button ItemButton => itemButton;
    public Image ItemImage => itemImage;
    public Sprite ClearItemImage => clearItemImage;

    public void Start()
    {
        if (itemButton == null)
            itemButton = GetComponent<Button>();
        if (itemImage == null)
            itemImage = GetComponent<Image>();
        if (clearItemImage == null)
            clearItemImage = GetComponent<Sprite>();
    }
}
