using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class PrimaryInvokeTutorialController : MonoBehaviour
{
    [SerializeField] private Grabbable grabbable;
    [SerializeField] private TutorialController tutorialController;

    private void Start()
    {
        grabbable.onGrab.AddListener((hand, grabbable1) => tutorialController.ShowPrimaryButton(grabbable, hand.left));
    }
}