using System.Collections;
using System.Collections.Generic;
using Common.DataManager;
using UnityEngine;
using UnityEngine.Events;

public class ActionTimerController : MonoBehaviour
{
    [SerializeField] private float timerCount;
    [SerializeField] private TimerController timer;
    [SerializeField] private UIPanelAnimation panelAnimation;
    [SerializeField] private UnityEvent onTimerIsEnd;

    public void StartTimer()
    {
        if (!SimulationManagerDataContainer.IsLightTrainingMode)
        {
            timer.StartTimer();
        }
    }
    public void StopTimer()
    {
        if (!SimulationManagerDataContainer.IsLightTrainingMode)
        {
            timer.StopTimer();
        }
    }
    public void PauseTimer() => timer.Pause();
    public void UnpauseTimer()
    {
        timer.Unpause();
    }
    public void EnableTimer()
    {
        if (!SimulationManagerDataContainer.IsLightTrainingMode)
        {
            timer.InitializeTimer(timerCount, onTimerIsEnd.Invoke);
            panelAnimation.EnablePanel();
        }
    }
    public void DisableTimer() => timer.DisableTimer();
}
