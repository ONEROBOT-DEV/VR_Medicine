using Autohand;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace TVP
{
    public class PositionManager : MonoBehaviour
    {
        Vector3 startPosition;
        Vector3 startRotation;
        Grabbable grababble;
        // Start is called before the first frame update
        void Start()
        {
            startPosition = transform.position;
            startRotation = transform.eulerAngles;
            grababble = GetComponent<Grabbable>();
            grababble.onRelease.AddListener(ResetPosition);
        }

        public void ResetPosition(Hand hand, Grabbable grabbable)
        {  
            if(GetComponentsInChildren<PositionManager>().Length>1)
            {
                foreach(var item in GetComponentsInChildren<PositionManager>())
                {
                    if(item != this)
                    {
                        item.HardReset();
                    }
                }
            }

            transform.position = startPosition;
            transform.eulerAngles = startRotation;
        }


        public void HardReset()
        {
            transform.SetParent(null);
            transform.position = startPosition;
            transform.eulerAngles = startRotation;
        }
    }
}