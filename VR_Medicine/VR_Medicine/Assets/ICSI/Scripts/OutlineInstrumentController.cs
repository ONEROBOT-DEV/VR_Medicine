using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class OutlineInstrumentController : MonoBehaviour
{
    [SerializeField] private MeshOutline[] meshes;

    [Button]
    private void FindAllOutlines()
    {
        meshes = GetComponentsInChildren<MeshOutline>(true);
    }

    public void SetEnable()
    {
        foreach (var meshOutline in meshes.Where(i => i != null))
            meshOutline.enabled = true;
    }

    public void SetDisable()
    {
        foreach (var meshOutline in meshes.Where(i => i != null))
            meshOutline.enabled = false;
    }
}
