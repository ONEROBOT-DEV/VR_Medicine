using System;
using Autohand;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Grabbable))]
public class Instrument : TrayInstrument
{
    [SerializeField] private Rigidbody rigidbody;
    [SerializeField] private Collider[] colliders;
    public Grabbable Grabbable;

    public void SetCollidersEnabled(bool value)
    {
        foreach (var collider in colliders)
        {
            collider.enabled = value;
        }
    }
    
    [Button]
    private void FindColliders()
    {
        colliders = GetComponentsInChildren<Collider>();
    }
}