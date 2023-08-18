using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DispenserTip : MonoBehaviour
{
    private bool isInteractable;
    public bool IsInteractable => isInteractable;
    public Action OnDisableAction;

    public void SetInteractable() => isInteractable = true;
    public void UnsetInteractable() => isInteractable = false;

    private void OnDisable()
    {
        OnDisableAction?.Invoke();
        //GetComponent<ActionInteractableObject>().InvokeEndAction();
    }
}