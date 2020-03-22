using UnityEngine;
using WildbotLabs.Scriptables.GameEvents;
using WildbotLabs.Scriptables.References;

public class SecondsTimer : MonoBehaviour
{

    public IntReference TimeValue;
    public GameEvent ResetTimeEvent;
    public GameEvent StopTimerEvent;

    public bool IsRunning = false;

    private float _runningTime;

    void OnEnable()
    {
        ResetTimeEvent?.RegisterListener(ResetTime);
        StopTimerEvent?.RegisterListener(StopTimer);
    }

    void OnDisable()
    {
        ResetTimeEvent?.UnregisterListener(ResetTime);
        StopTimerEvent?.RegisterListener(StopTimer);
    }

    void Update()
    {
        if (IsRunning)
        {
            _runningTime += Time.deltaTime;
            TimeValue?.SetValue((int) Mathf.Floor(_runningTime));
        }
    }

    private void ResetTime()
    {
        TimeValue?.SetValue(0);
        _runningTime = 0;
        IsRunning = true;
    }

    private void StopTimer()
    {
        IsRunning = false;
    }
}
