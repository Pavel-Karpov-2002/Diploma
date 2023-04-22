using System;

public class TimerScript
{
    private DateTime timeBegin = DateTime.Now;
    private bool isPaused = true;
    private float durationTime;

    public bool IsPaused => isPaused;
    private TimeSpan TimeInterval => DateTime.Now - timeBegin;

    public void Start(float durationTime)
    {
        timeBegin = DateTime.Now;
        this.durationTime = durationTime;
    }

    public float GetRemainingTime()
    {
        return durationTime - TimeInterval.Seconds;
    }
}
