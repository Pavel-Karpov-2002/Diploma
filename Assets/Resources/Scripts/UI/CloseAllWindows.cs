using System.Collections.Generic;
using UnityEngine;

public class CloseAllWindows : MonoBehaviour
{
    [SerializeField] private List<GameObject> windows;

    private void Start()
    {
        foreach (var window in windows)
        {
            window.SetActive(false);
        }
    }
}
