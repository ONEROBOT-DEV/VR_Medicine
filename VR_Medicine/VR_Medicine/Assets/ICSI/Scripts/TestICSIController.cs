using System;
using System.Collections;
using System.Collections.Generic;
using Common.DataManager;
using ICSI.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;

public class TestICSIController : MonoBehaviour
{
    [SerializeField] private MeshOutlineData[] meshOutlineDatas;

    [Button]
    private void FindDatas()
    {
        var meshOutlines = GetComponentsInChildren<MeshOutline>(true);
        meshOutlineDatas = new MeshOutlineData[meshOutlines.Length];

        for (var i = 0; i < meshOutlines.Length; i++)
        {
            meshOutlineDatas[i] =
                new MeshOutlineData(meshOutlines[i], meshOutlines[i].GetComponent<MeshOutlineAnimator>());
        }
    }
    
    private void Start()
    {
        if (SimulationManagerDataContainer.IsTestMode)
        {
            foreach (var outlineData in meshOutlineDatas)
            {
                Destroy(outlineData.Animator);
                Destroy(outlineData.MeshOutline);
            }
        }
    }
}

[Serializable]
public class MeshOutlineData
{
    public MeshOutline MeshOutline;
    public MeshOutlineAnimator Animator;

    public MeshOutlineData(MeshOutline meshOutline, MeshOutlineAnimator animator)
    {
        MeshOutline = meshOutline;
        Animator = animator;
    }
}