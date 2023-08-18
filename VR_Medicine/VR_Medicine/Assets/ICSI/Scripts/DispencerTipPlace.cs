using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class DispencerTipPlace : Instrument
{
    [SerializeField] private DispenserTip[] tips;
    [SerializeField] private UnityEvent onTipPutOn;

    [Button]
    private void FindTips()
    {
        tips = GetComponentsInChildren<DispenserTip>(true);
    }

    private void Start()
    {
        foreach (var tip in tips)
        {
            tip.OnDisableAction = onTipPutOn.Invoke;
        }
    }

    public void ResetTips()
    {
        foreach (var tip in tips)
        {
            tip.gameObject.SetActive(true);
        }
    }

    public void SetInteractable()
    {
        foreach (var tip in tips)
        {
            tip.SetInteractable();
        }
    }

    public void DisableInteractable()
    {
        foreach (var tip in tips)
        {
            tip.UnsetInteractable();
        }
    }
}
