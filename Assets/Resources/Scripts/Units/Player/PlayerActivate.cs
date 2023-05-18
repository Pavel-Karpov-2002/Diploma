using UnityEngine;

public class PlayerActivate : PlayerConstructor
{
    [SerializeField] protected GameParameters gameParameters;
    [SerializeField] protected AudioParameters audioParameters;

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
                PlayAudio(audioParameters.OpeningInventory);
                break;
            }
        }
    }

    protected void PlayAudio(AudioClip clip)
    {
        AudioController.Instance.PlayOneAudio(clip);
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
