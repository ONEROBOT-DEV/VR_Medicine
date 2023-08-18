using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class EditorButtonInvoke : MonoBehaviour
{
    [Button]
    private void InvokeClickButton()
    {
        GetComponent<Button>().onClick.Invoke();
    }
}