using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI textMeshPro;

    public Button Button => button;
    public Image Image => image;
    public TextMeshProUGUI TextMeshPro => textMeshPro;
    public int AmountUses { get; set; }
}
