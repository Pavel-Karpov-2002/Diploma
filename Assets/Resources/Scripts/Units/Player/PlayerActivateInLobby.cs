using UnityEngine;

public class PlayerActivateInLobby : PlayerActivate
{
    [SerializeField] private FacultyParameters facultyParameters;
    [SerializeField] private LinkParameters linkParameters;
    [SerializeField] private GameObject settingsWindow;

    private bool isWindowActive;

    public void OpenSkinChangeWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        var trigger = Physics2D.OverlapCircle(transform.position, PlayerParameters.DistanceForActivateSkinChangeWindow, PlayerParameters.ChangeSkinLayer);
        if (!trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void OpenFacultyLevelWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        var trigger = Physics2D.OverlapCircle(transform.position, facultyParameters.DistanceActivateFacultyPanel, facultyParameters.LayerActivateFacultyPanel);
        if (!trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void OpenLinkLevelWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        var trigger = Physics2D.OverlapCircle(transform.position, linkParameters.DistanceActivateLinkPanel, linkParameters.LayerActivateLinkPanel);
        if (!trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void OpenStoriesWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        var trigger = Physics2D.OverlapCircle(transform.position, PlayerParameters.DistanceForActivateStoryWindow, PlayerParameters.StoryLayer);
        if (!trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void CloseWindow(GameObject window)
    {
        ChangeWindowActive(window, false);
    }

    public void GetNewItem(GameObject window)
    {
        if (isWindowActive)
            return;
        ItemParameters itemParameters = PlayerParameters.ItemStatusWindow;
        var trigger = Physics2D.OverlapCircle(transform.position, itemParameters.DistanceActivateItemDrop, itemParameters.LayerActivateItemDrop);
        if (!trigger)
            return;
        if (GameData.Data.AmountMoney - PlayerParameters.CostRollingItems <= 0)
        {
            GameException.Instance.ShowError("У Вас недостаточно кристаллов!");
            return;
        }
        OperationWithItems.Instance.GetRandomItem();
        ChangeWindowActive(window, true);
        PlayAudio(audioParameters.OpeningBackpack);
        GameData.Data.AmountMoney -= PlayerParameters.CostRollingItems;
        PlayerStatsInformation.Instance.UpdateInformationText();
#if UNITY_EDITOR
        string path = Application.streamingAssetsPath;
#elif UNITY_ANDROID             
        string path = Application.persistentDataPath;
#endif
        GameData.UpdateGameDataFile(path + gameParameters.DataPath);
    }

    public void OpenStatusWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        PlayAudio(audioParameters.OpeningInventory);
        int ignoredMask = ~LayerMask.GetMask("Default");
        var triggers = Physics2D.OverlapCircleAll(transform.position, 3, ignoredMask);
        if (triggers.Length == 0)
            return;

        foreach (var trigger in triggers)
            if (trigger.gameObject != gameObject)
                return;

        ChangeWindowActive(window, true);
    }

    public void CloseStatusWindow(GameObject window)
    {
        ChangeWindowActive(window, false);
    }

    private void ChangeWindowActive(GameObject window, bool active)
    {
        isWindowActive = active;
        window.SetActive(active);
        settingsWindow.SetActive(!active);
        MovementJoystick.Instance.gameObject.SetActive(!active);
    }
}
