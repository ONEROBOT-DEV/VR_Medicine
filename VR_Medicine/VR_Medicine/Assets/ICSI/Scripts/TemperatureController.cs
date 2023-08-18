using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class TemperatureController : ActionInteractableObject
{
    private const string ADD_STRING_TEMPRETURE = "°C";
    [SerializeField] private int minTemperatureValue;
    [SerializeField] private int maxTemperatureValue;
    [SerializeField] private float stepTemperature;
    [SerializeField] private TMP_Text counter;
    [SerializeField] private float temperature = 35;
    private bool isTrueValue;

    public void ResetTemperature()
    {
        temperature = 35;
        SetValue();
    }

    public void OnClickDone()
    {
        if (!isTrueValue)
        {
            counter.color = Color.red;
            counter.DOColor(Color.white, 1);
            return;
        }
        
        counter.color = Color.green;
        counter.DOColor(Color.white, 1);
        InvokeEndAction();
    }

    public void AddTemperatures(int multiply)
    {
        temperature += multiply * stepTemperature;
        SetValue();
    }

    private void Start()
    {
        SetValue();
    }

    private void SetValue()
    {
        counter.text = Math.Round(Mathf.Abs(temperature), 1) + ADD_STRING_TEMPRETURE;
        isTrueValue = temperature > minTemperatureValue && temperature < maxTemperatureValue;
    }
}