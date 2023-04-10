using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float questionTime;
    private float leftTime;
    private IEnumerator startTimer;

    public float QustionTime { get { return questionTime; } set { questionTime = value; } }

    public void StartTimer()
    {
        startTimer = TimeChanging();
        StartCoroutine(startTimer);
    }

    private IEnumerator TimeChanging()
    {
        yield return new WaitForSeconds(0.1f);
        if(leftTime <= questionTime)
        {
            leftTime += 0.1f;
            StartTimer();
        }
        else
        {
            leftTime = 0;
        }
    }
}
