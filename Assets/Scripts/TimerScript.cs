using System;
using TMPro;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    private TMP_Text timerText;
    enum TimerType { Countdown, Stopwatch }
    [SerializeField] private TimerType timerType;

    [SerializeField] private float timeToDisplay = 0.0f;
    private bool isRunning;

    private void Awake()
    {
        timerText = GetComponent<TMP_Text>();
    }

    private void OnEnable()
    {
        EventManager.TimerStart += EventManagerOnTimerStart;
        EventManager.TimerStop += EventManagerOnTimerStop;
        EventManager.TimerUpdate += EventManagerOnTimerUpdate;
    }

    private void OnDisable()
    {
        EventManager.TimerStart -= EventManagerOnTimerStart;
        EventManager.TimerStop -= EventManagerOnTimerStop;
        EventManager.TimerUpdate -= EventManagerOnTimerUpdate;
    }

    private void EventManagerOnTimerStart() => isRunning = true;
    private void EventManagerOnTimerStop() => isRunning = false;
    private void EventManagerOnTimerUpdate(float value) => timeToDisplay += value;

    void Update()
    {
        if (!isRunning)
        {
            return;
        }
        if (timerType == TimerType.Stopwatch && timeToDisplay > 300.0f)
        {
            EventManager.OnTimerStop();
            return;
        }
        timeToDisplay += timerType == TimerType.Countdown ? -Time.deltaTime : Time.deltaTime;

        TimeSpan timeSpan = TimeSpan.FromSeconds(timeToDisplay);
        timerText.text = timeSpan.ToString(@"mm\:ss\:fff");
    }
}