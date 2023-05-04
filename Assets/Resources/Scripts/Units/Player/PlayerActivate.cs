using UnityEditor.PackageManager.UI;
using UnityEngine;

public class PlayerActivate : PlayerConstructor
{ 
    public void StartSayingWithNPC()
    {
        var trigger = Physics2D.OverlapCircle(transform.position, GameParameters.Player.NPCTriggerDistance, GameParameters.Player.NPCLayer);
        
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
            DialogScript.GetInstance().ShowNewQuestion();
        }
    }

    public void GetNewItem()
    {
        ItemParameters itemParameters = GameParameters.ItemStatusWindow;
        var trigger = Physics2D.OverlapCircle(transform.position, itemParameters.DistanceActivateItemDrop, itemParameters.LayerActivateItemDrop);

        if (!trigger)
            return;

        OperationWithItems.GetInstance().GetRandomItem();
    }

    public void OpenStatusWindow(GameObject window)
    {
        ChangeWindowAndJoystickActive(window, true);
    }

    public void CloseStatusWindow(GameObject window)
    {
        ChangeWindowAndJoystickActive(window, false);
    }

    private void ChangeWindowAndJoystickActive(GameObject window, bool active)
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
