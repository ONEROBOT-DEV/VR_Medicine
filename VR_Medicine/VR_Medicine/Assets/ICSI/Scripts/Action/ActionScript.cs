using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.DataManager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public abstract class ActionScript : MonoBehaviour
{
    [SerializeField] private ActionEventData actionEventData;
    [SerializeField] private UnityEvent onStartAction;
    [SerializeField] protected UnityEvent onEndAction;
    [SerializeField] private UnityEventData onRestartAction;
    [SerializeField] protected PartActionObject[] parts;
    protected Action<ActionScript> onActionPartEnd;
    protected List<PartActionObject> listenedPartAction = new();
    public List<ActionInteractableObject> listenedObjects = new();
    private bool isInitialize;

    [Serializable]
    public class UnityEventData
    {
        public UnityEvent Event;
        public MeshOutline[] Outlines;
    }

    private void OnValidate()
    {
        actionEventData.OnValidate();
    }

    [Button]
    private void FindInteractable(Transform parent)
    {
        var result = new List<MeshOutline>(onRestartAction.Outlines);
        result.AddRange(parent.GetComponentsInChildren<MeshOutline>(true));
        onRestartAction.Outlines = result.ToArray();
    }

    public virtual ActionScript Initialize()
    {
        onStartAction.Invoke();
        isInitialize = true;

        foreach (var part in parts)
        {
            listenedPartAction.Add(part);
        }

        InitializeObjectInPart();

        if (actionEventData.type.Contains(ActionTypeErrors.StartLevel))
            SimulationStaticDataManager.StartLevel(actionEventData.Level);

        return this;
    }

    public void StopListeningThisAction()
    {
        //listenedPartAction[0].OnEndPart?.Invoke();
        onActionPartEnd = null;
        isInitialize = false;
        listenedPartAction.Clear();
        listenedObjects.Clear();
    }

    public void RestartAction(int startActionsIndex)
    {
        onRestartAction.Event.Invoke();

        if (actionEventData.type.Contains(ActionTypeErrors.ErrorLevel))
        {
            SimulationStaticDataManager.LevelIsPassedWithError(actionEventData.Level, actionEventData.ErrorText);
        }

        listenedObjects.Clear();
        listenedPartAction.Clear();
        isInitialize = true;
        foreach (var interactableObject in onRestartAction.Outlines)
        {
            interactableObject.enabled = false;
        }

        for (var index = 0; index < startActionsIndex; index++)
        {
            parts[index].OnStartPart?.Invoke();
            parts[index].OnEndPart?.Invoke();
        }

        for (var index = startActionsIndex; index < parts.Length; index++)
        {
            listenedPartAction.Add(parts[index]);
        }

        InitializeObjectInPart();
    }

    protected void InitializeObjectInPart()
    {
        listenedPartAction[0].OnStartPart?.Invoke();
        foreach (var interactableObject in listenedPartAction[0].objects)
        {
            interactableObject.Initialize().OnComplete(() => OnInteractableObjectEndAction(interactableObject));
            listenedObjects.Add(interactableObject);
        }
    }

    public ActionScript OnComplete(Action<ActionScript> action)
    {
        onActionPartEnd = action;
        return this;
    }

    [Button]
    private void InvokeEndAction()
    {
        if (listenedObjects.Any())
            OnInteractableObjectEndAction(listenedObjects[0]);
    }

    protected void OnInteractableObjectEndAction(ActionInteractableObject obj)
    {
        if (!isInitialize) return;
        obj.ClearAction();
        listenedObjects.Remove(obj);

        if (listenedObjects.Count == 0)
        {
            listenedPartAction[0].OnEndPart?.Invoke();
            listenedPartAction.RemoveAt(0);

            if (listenedPartAction.Count == 0)
            {
                if (actionEventData.type.Contains(ActionTypeErrors.FinishLevel))
                {
                    SimulationStaticDataManager.LevelIsPassedWithoutError(actionEventData.Level);
                }

                onEndAction?.Invoke();
                onActionPartEnd?.Invoke(this);
            }
            else
            {
                OnStartInteractable();
                InitializeObjectInPart();
            }
        }
    }

    protected virtual void OnStartInteractable()
    {
    }
}

public enum ActionTypeErrors
{
    StartLevel,
    FinishLevel,
    ErrorLevel
}

[Serializable]
public class ActionEventData
{
    public ActionTypeErrors[] type;
    public int Level;
    public string ErrorText;

    public void OnValidate()
    {
        if (type.Length > 3) type = new ActionTypeErrors[3];
        for (ActionTypeErrors i = 0; (int)i < type.Length; i++)
        {
            type[(int)i] = i;
        }
    }
}

[Serializable]
public class PartActionObject
{
    public UnityEvent OnStartPart;
    public UnityEvent OnEndPart;
    public ActionInteractableObject[] objects;
}