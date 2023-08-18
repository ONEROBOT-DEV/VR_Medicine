using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class OnMainUIPanelEnabled : MonoBehaviour
{
    [SerializeField] private HandModelData[] hands;
    private readonly Dictionary<GameObject, int> defaultLayers = new();
    private readonly Dictionary<HandModelData, HandGrabReleaseEvent> handInteractables = new();

    public void SetDefaultLayer()
    {
        foreach (var handModelData in hands)
        {
            if (!handInteractables.ContainsKey(handModelData)) return;
            
            handModelData.Hand.OnGrabbed -= handInteractables[handModelData].onGrab;
            handModelData.Hand.OnReleased -= handInteractables[handModelData].onRelease;

            foreach (var model in handModelData.Models)
            {
                model.layer = defaultLayers[model];
            }
        }
    }

    public void SetOverlayLayer()
    {
        foreach (var handModelData in hands)
        {
            if (!handInteractables.ContainsKey(handModelData)) return;
            
            handModelData.Hand.OnGrabbed += handInteractables[handModelData].onGrab;
            handModelData.Hand.OnReleased += handInteractables[handModelData].onRelease;

            if (handModelData.Hand.holdingObj != null) continue;

            SetHandOverlayLayer(handModelData);
        }
    }

    private void SetHandDefaultLayer(HandModelData handData)
    {
        foreach (var model in handData.Models)
        {
            model.layer = defaultLayers[model];
        }
    }

    private void SetHandOverlayLayer(HandModelData handData)
    {
        var layerUI = LayerMask.NameToLayer("OverlayObjects");

        foreach (var model in handData.Models)
        {
            model.layer = layerUI;
        }
    }

    private void Awake()
    {
        foreach (var handModelData in hands)
        {
            foreach (var model in handModelData.Models)
            {
                defaultLayers.Add(model, model.layer);
            }

            handInteractables.Add(handModelData, new HandGrabReleaseEvent(
                (_, _) => SetHandDefaultLayer(handModelData),
                (_, _) => SetHandOverlayLayer(handModelData)));
        }
    }
}

[Serializable]
public class HandGrabReleaseEvent
{
    public HandGrabEvent onGrab;
    public HandGrabEvent onRelease;

    public HandGrabReleaseEvent(HandGrabEvent onGrab, HandGrabEvent onRelease)
    {
        this.onGrab = onGrab;
        this.onRelease = onRelease;
    }
}

[Serializable]
public class HandModelData
{
    public GameObject[] Models;
    public Hand Hand;
}