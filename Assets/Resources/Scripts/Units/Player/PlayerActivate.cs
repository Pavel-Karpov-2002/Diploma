using UnityEngine;

public class PlayerActivate : PlayerConstructor
{ 
    public void StartSayingWithNPC()
    {
        var trigger = Physics2D.OverlapCircle(transform.position, PlayerParameters.NPCTriggerDistance, PlayerParameters.NPCLayer);
        
        if (!trigger)
            return;
        
        NPC npc = trigger.gameObject.GetComponent<NPC>();

        if (npc.IsExpectation)
        {
            npc.SetExpectation(false);
            if (npc.IsExpectation)
                return;
            MovementJoystick.GetInstance().gameObject.SetActive(false);
            DialogPanelSingleton.GetInstance().gameObject.SetActive(true);
        }
    }

    public void OpenLevelWindow(GameObject window)
    {
        FacultyParameters facultyParameters = FacultyPanelScript.GetInstance().FacultyParameters;
        var trigger = Physics2D.OverlapCircle(transform.position, facultyParameters.DistanceActivateFacultyPanel, facultyParameters.LayerActivateFacultyPanel);
        if (!trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void CloseLevelWindow(GameObject window)
    {
        ChangeWindowActive(window, false);
    }

    public void GetNewItem()
    {
        ItemParameters itemParameters = PlayerParameters.ItemStatusWindow;
        var trigger = Physics2D.OverlapCircle(transform.position, itemParameters.DistanceActivateItemDrop, itemParameters.LayerActivateItemDrop);
        if (!trigger)
            return;
        OperationWithItems.GetInstance().GetRandomItem();
    }

    public void OpenStatusWindow(GameObject window)
    {
        FacultyParameters facultyParameters = FacultyPanelScript.GetInstance().FacultyParameters;
        var trigger = Physics2D.OverlapCircle(transform.position, facultyParameters.DistanceActivateFacultyPanel, facultyParameters.LayerActivateFacultyPanel);
        if (trigger)
            return;
        ChangeWindowActive(window, true);
    }

    public void CloseStatusWindow(GameObject window)
    {
        ChangeWindowActive(window, false);
    }

    private void ChangeWindowActive(GameObject window, bool active)
    {
        window.SetActive(active);
        MovementJoystick.GetInstance().gameObject.SetActive(!active);
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
