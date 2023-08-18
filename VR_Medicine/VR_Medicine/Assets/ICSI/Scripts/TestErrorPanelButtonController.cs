using System;
using System.Collections;
using System.Collections.Generic;
using Common.DataManager;
using Common.UI;
using UnityEngine;
using UnityEngine.UI;

public class TestErrorPanelButtonController : MonoBehaviour
{
    [SerializeField] private Button buttonReload;
    [SerializeField] private Button buttonDefault;

    public void OnClickReload()
    {
        SimSceneManager.CurrentSimulation.LoadICSI();
    }

    private void Start()
    {
        if (!SimulationManagerDataContainer.IsTestMode)
        {
            //buttonReload.gameObject.SetActive(false);
            Destroy(buttonReload.gameObject);
            Destroy(this);
        }
        else
        {
            Destroy(buttonDefault.gameObject);
        }
    }
}