using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerSetter : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeLayer", 2);
    }
     
    public void ChangeLayer()
    {
        gameObject.layer = 9;
    }
}
