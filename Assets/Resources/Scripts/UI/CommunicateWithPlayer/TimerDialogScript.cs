using UnityEngine;
using UnityEngine.UI;

public class TimerDialogScript : Singleton<TimerDialogScript>
{
    [SerializeField] private Slider timerUI;

    private TimerScript timer;
    private float timeDuration;

    public bool IsPaused { get; set; } = true;
    public float TimeDuration { get { return timeDuration; } set { timeDuration = value; } }
    public float TimeRemaining => timer.GetRemainingTime();

    public delegate void TimeEnd();

    public static TimeEnd TimerEnd;

    private void Update()
    {
        if (IsPaused)
            return;

        if (timer.GetRemainingTime() < 0)
        {
            IsPaused = true;
            TimerEnd?.Invoke();
        }

        timerUI.value = (timer.GetRemainingTime() / timeDuration);
    }

    public void StartTimer(float timeDuration)
    {
        timer = new();
        this.timeDuration = timeDuration;
        timer.Start(this.timeDuration);
        IsPaused = false;
    }

    private void OnDestroy()
    {
        TimerEnd = null;
    }
}
