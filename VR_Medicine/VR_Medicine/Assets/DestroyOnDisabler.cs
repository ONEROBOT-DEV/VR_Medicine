using System;
using System.Collections;
using System.Collections.Generic;
using Autohand;
using UnityEngine;

public class DestroyOnDisabler : MonoBehaviour
{
    private Grabbable placeGrabbable;

    private void Start()
    {
        var point = GetComponent<PlacePoint>();
        if (point != null && point.matchTarget != null)
        {
            placeGrabbable = point.matchTarget;
        }
    }

    private void OnDisable()
    {
        if (placeGrabbable != null && !placeGrabbable.IsHeld())
        {
            placeGrabbable.transform.SetParent(transform.parent);
        }
        Destroy(gameObject);
    }
}
