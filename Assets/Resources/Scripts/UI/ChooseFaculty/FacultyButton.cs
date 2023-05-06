using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FacultyButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI facultyText;
    [SerializeField] private Button button;

    public TextMeshProUGUI FacultyText => facultyText;
    public Button Button => button;
}
