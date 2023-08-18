using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class InstrumentDescriptionHighlightController : MonoBehaviour
{
    [SerializeField] private TrayInstrumentHighlight instrumentHighlight;
    [SerializeField] private Grabbable grabbable;

    private void OnValidate()
    {
        if (instrumentHighlight == null)
        {
            instrumentHighlight = FindObjectOfType<TrayInstrumentHighlight>();
        }
        if (grabbable == null)
        {
            grabbable = GetComponent<Grabbable>();
        }
    }

    private void Start()
    {
        grabbable.OnHighlightEvent += OnHighlight;
        grabbable.OnUnhighlightEvent += OnUnhighlight;
    }

    private void OnHighlight(Hand hand, Grabbable grabbable)
    {
        instrumentHighlight.OnHighlightGrabbable(grabbable);
    }

    private void OnUnhighlight(Hand hand, Grabbable grabbable)
    {
        instrumentHighlight.OnExitHighlightGrabbable(grabbable);
    }
}