using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    [SerializeField] private Image image;

    public TextMeshProUGUI ButtonText => buttonText;
    public Button Button => button;
    public Image Image => image;
}
