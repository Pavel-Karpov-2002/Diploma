using UnityEngine;

public class PlayerActivate : PlayerConstructor
{
    [SerializeField] private GameParameters gameParameters;
    private bool isWindowActive;

    public void StartSayingWithNPC()
    {
        var trigger = Physics2D.OverlapCircleAll(transform.position, PlayerParameters.NPCTriggerDistance, PlayerParameters.NPCLayer);
        
        if (trigger.Length == 0)
            return;
        
        foreach (var npcTrigger in trigger)
        {
            NPC npc = npcTrigger.gameObject.GetComponent<NPC>();

            if (npc.IsExpectation)
            {
                npc.SetExpectation(false);
                if (npc.IsExpectation)
                    return;
                MovementJoystick.Instance.gameObject.SetActive(false);
                DialogScript.Instance.gameObject.SetActive(true);
                ScoresUI.Instance.ChangeAlphaPanel(0);
                break;
            }
        }
    }

    public void OpenSkinChangeWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        var trigger = Physics2D.OverlapCircle(transform.position, PlayerParameters.DistanceForActivateSkinChangeWindow, PlayerParameters.ChangeSkinLayer);
        if (!trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void OpenLevelWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        FacultyParameters facultyParameters = FacultyPanelScript.Instance.FacultyParameters;
        var trigger = Physics2D.OverlapCircle(transform.position, facultyParameters.DistanceActivateFacultyPanel, facultyParameters.LayerActivateFacultyPanel);
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
            Debug.Log("У Вас не достаточно кристаллов");
            return;
        }
        OperationWithItems.Instance.GetRandomItem();
        ChangeWindowActive(window, true);
        GameData.Data.AmountMoney -= PlayerParameters.CostRollingItems;
        PlayerStatsInformation.Instance.UpdateInformationText();
        GameData.UpdateGameDataFile(gameParameters.DataPath);
    }

    public void OpenStatusWindow(GameObject window)
    {
        if (isWindowActive)
            return;
        int ignoredMask = ~LayerMask.GetMask("Default");
        var triggers = Physics2D.OverlapCircleAll(transform.position, 3, ignoredMask);

        if (triggers.Length == 0)
            return;

        foreach (var trigger in triggers)
        {
            if (trigger.gameObject != gameObject)
                return;
        }

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
        MovementJoystick.Instance.gameObject.SetActive(!active);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if (PlayerParameters != null)
            Gizmos.DrawWireSphere(transform.position, PlayerParameters.NPCTriggerDistance);
    }
#endif
}
