using System.Collections;
using System.Collections.Generic;
using Autohand;
using ICSI.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class InteractableInstrumentContainer : MonoBehaviour
{
    public MeshOutline MeshOutline;
    public MeshOutlineAnimator MeshOutlineAnimator;
    public Grabbable Grabbable;
    public Dispenser Dispenser;

    [Button]
    private void Initialize()
    {
        MeshOutline = GetComponentInChildren<MeshOutline>();
        MeshOutlineAnimator = GetComponentInChildren<MeshOutlineAnimator>();
        Grabbable = GetComponentInChildren<Grabbable>();
        Dispenser = GetComponentInChildren<Dispenser>();
    }
}