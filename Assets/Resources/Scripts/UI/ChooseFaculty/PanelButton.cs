using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;

    public TextMeshProUGUI ButtonText => buttonText;
    public Button Button => button;
}
