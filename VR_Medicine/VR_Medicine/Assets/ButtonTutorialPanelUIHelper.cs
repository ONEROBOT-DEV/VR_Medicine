using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;

public class ButtonTutorialPanelUIHelper : MonoBehaviour, IOculusControllerButtonDownListener, IOculusControllerButtonUpListener
{
    [SerializeField] private UIPanelAnimation panelAnimator;
    [SerializeField] private HandButtonHandler handListener;

    private void Start()
    {
        bool find = false;
        foreach (var buttonHandler in handListener.GetComponents<HandButtonHandler>())
        {
            if (buttonHandler.GetHandlerButton == CommonButton.menuButton)
            {
                buttonHandler.OnButtonDown = OnClickPrimaryButtonDown;
                buttonHandler.OnButtonUp = OnClickPrimaryButtonUp;
                find = true;
                break;
            }
        }
        
        if (!find) Debug.LogError("Не нашел слушателя нужной кнопки! :(", handListener);
        
        gameObject.SetActive(false);
    }

    public void OnClickPrimaryButtonDown(Hand hand, CommonButton button)
    {
        if (button == CommonButton.menuButton) panelAnimator.EnablePanel();
    }

    public void OnClickPrimaryButtonUp(Hand hand, CommonButton button)
    {
        if (button == CommonButton.menuButton) panelAnimator.DisablePanel();
    }
}
