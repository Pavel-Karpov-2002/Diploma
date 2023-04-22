using UnityEngine;
using UnityEngine.UI;

public class TimerDialogScript : MonoBehaviour
{
    [SerializeField] private Slider timerUI;

    private TimerScript timer;
    private float timeDuration;

    public bool IsPaused { get; set; }

    private static TimerDialogScript instance;
    public delegate void TimeEnd();

    public static event TimeEnd TimerEnd;
    
    public void StartTimer(float timeDuration)
    {
        if (instance == null)
            instance = this;

        IsPaused = false;
        timer = new();
        this.timeDuration = timeDuration;
        timer.Start(this.timeDuration);
    }

    private void FixedUpdate()
    {
        if (IsPaused)
            return;

        if (timer.GetRemainingTime() < 0)
            TimerEnd?.Invoke();

        timerUI.value = (timer.GetRemainingTime() / timeDuration);
    }

    public static TimerDialogScript GetInstance()
    {
        if (instance == null)
            instance = Resources.FindObjectsOfTypeAll<TimerDialogScript>()[0];

        return instance;
    }


    private void OnDestroy()
    {
        TimerEnd = null;
        instance = null;
    }
}
