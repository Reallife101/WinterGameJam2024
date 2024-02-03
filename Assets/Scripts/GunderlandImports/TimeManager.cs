using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] AnimationCurve timeStopCurve;
    public static TimeManager Instance { get; private set; }
    private float slowTimer;
    private float slowTimerTotal;

    // Start is called before the first frame update

    private void Awake()
    {

        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

    public void TimeSlow(float time)
    {
        slowTimer = time;
        slowTimerTotal = time;
    }

    private void Update()
    {
        if (slowTimer > 0)
        {
            slowTimer -= Time.unscaledDeltaTime;
            Time.timeScale = timeStopCurve.Evaluate(slowTimer / slowTimerTotal);
        }
    }
}
