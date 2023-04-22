using UnityEngine;

public class PlayerActivate : PlayerConstructor
{
    public void StartSayingWithNPC()
    {
        var trigger = Physics2D.OverlapCircle(transform.position, GameParameters.Player.NPCTriggerDistance, GameParameters.Player.NPCLayer);

        if (trigger && trigger.gameObject.GetComponent<NPC>().IsWaiting)
        {
            MovementJoystick.GetInstance().gameObject.SetActive(false);
            DialogPanelSingleton.GetInstance().gameObject.SetActive(true);
            DialogScript.GetInstance().ShowNewQuestion();
            trigger.gameObject.GetComponent<NPC>().IsWaiting = false;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (PlayerParameters != null)
        {
            Gizmos.DrawWireSphere(transform.position, PlayerParameters.NPCTriggerDistance);
        }
    }
#endif
}
