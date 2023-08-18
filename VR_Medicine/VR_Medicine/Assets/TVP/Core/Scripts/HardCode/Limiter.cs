using System.Collections;
using System.Collections.Generic;
using TVP;
using UnityEngine;

public class Limiter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PositionManager>())
        {
          //  other.gameObject.GetComponent<PositionManager>().ResetPosition();
        }
    }
}
