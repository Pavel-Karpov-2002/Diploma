#if UNITY_EDITOR
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeQuestionButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Button deleteButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    public Button Button => button;
    public Button DeleteButton => deleteButton;
    public TextMeshProUGUI ButtonText => buttonText;
}
#endif