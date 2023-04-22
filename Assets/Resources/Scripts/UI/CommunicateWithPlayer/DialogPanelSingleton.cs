using UnityEngine;

public class DialogPanelSingleton : MonoBehaviour
{
    private static DialogPanelSingleton instance;

    private void Start()
    {
        if (instance == null)
            instance = this;

        gameObject.SetActive(false);
    }

    public static DialogPanelSingleton GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<DialogPanelSingleton>()[0];

        return instance;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
