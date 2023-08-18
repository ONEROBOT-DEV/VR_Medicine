using System;
using System.Collections;
using System.Collections.Generic;
using Common.DataManager;
using Common.UI;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

public class ActionControllers : MonoBehaviour
{
    [SerializeField] private ActionScript[] actions;
    private int counter;
    private float startTimer;

    [Button]
    public void FindActions()
    {
        actions = GetComponentsInChildren<ActionScript>();
    }

    public void ReturnToBackAction(int index)
    {
        actions[counter].OnComplete(null);
        counter = index;
        StartAction();
    }

    private IEnumerator Start()
    {
        counter = 0;
        yield return new WaitForSeconds(2.5f);
        StartAction();
        startTimer = Time.time;
    }

    private void StartAction()
    {
        actions[counter].Initialize()
            .OnComplete(OnActionEnded);
    }

    private void OnActionEnded(ActionScript action)
    {
        if (actions[counter] != action) return;

        counter++;
        if (counter >= actions.Length)
        {
            for (var i = 0; i <= 12; i++)
            {
                SimulationStaticDataManager.StartLevel(i);
                SimulationStaticDataManager.CheckLevelIsPassedWithoutError(i);
                SimulationStaticDataManager.FinishLevel(i);
            }

            SimulationStaticDataManager.SimulationIsFinished(Time.time - startTimer);
            SimSceneManager.CurrentSimulation.LoadShowResult();
            return;
        }

        StartAction();
    }
}