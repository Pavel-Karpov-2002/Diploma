using UnityEngine;

[CreateAssetMenu(fileName = "LinkParameters", menuName = "CustomParameters/LinkParameters")]
public class LinkParameters : ScriptableObject
{
    [SerializeField] private string folderPath;
    [SerializeField] private float distanceActivateLinkPanel;
    [SerializeField] private LayerMask layerActivateLinkPanel;

    public string FolderPath => folderPath;
    public float DistanceActivateLinkPanel => distanceActivateLinkPanel;
    public LayerMask LayerActivateLinkPanel => layerActivateLinkPanel;
}
