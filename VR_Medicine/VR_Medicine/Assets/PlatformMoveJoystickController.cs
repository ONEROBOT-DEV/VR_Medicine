using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using Autohand.Demo;
using UnityEngine;
using UnityEngine.UI;

public class PlatformMoveJoystickController : ActionInteractableObject
{
    public Transform MicroWorld;
    public Transform Egg;
    public XRHandPlayerControllerLink XRHandPlayerControllerLink;
    public Grabbable Grabbable;
    public MeshOutline Outline;
    public XRHandControllerLink HandControllerLink;
    private bool isGrab;
    private Vector3 targetEggPosition;

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(MicroWorld.position, .1f);
        Gizmos.DrawWireSphere(Egg.position, .1f);
        Gizmos.DrawLine(MicroWorld.position, Egg.position);
    }

    public void OnEnd()
    {
        Grabbable.enabled = false;
        Grabbable.OnGrabEvent -= OnGrab;
        Grabbable.OnReleaseEvent -= OnRelease;
        Outline.enabled = false;
        isGrab = false;
        enabled = false;
    }

    public void OnStart()
    {
        Grabbable.enabled = true;
        Grabbable.OnGrabEvent += OnGrab;
        Grabbable.OnReleaseEvent += OnRelease;
        Outline.enabled = true;
        targetEggPosition = MicroWorld.position;
        enabled = true;
    }

    private void OnGrab(Hand hand, Grabbable grabbable)
    {
        isGrab = true;
        XRHandPlayerControllerLink.enabled = false;
    }

    private void OnRelease(Hand hand, Grabbable grabbable)
    {
        isGrab = false;
        XRHandPlayerControllerLink.enabled = true;
    }

    private void Update()
    {
        if (!isGrab) return;
        
        var inputValue = Mathf.Abs(HandControllerLink.GetAxis2D(Common2DAxis.primaryAxis).y);
        
        MicroWorld.transform.position -= Vector3.forward * (0.1f * inputValue * Time.deltaTime);

        if ((Egg.position - targetEggPosition).sqrMagnitude < .0001f)
        {
            InvokeEndAction();
            Egg.position = targetEggPosition;
        }
    }
}
