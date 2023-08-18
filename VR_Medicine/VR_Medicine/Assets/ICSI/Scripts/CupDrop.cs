using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupDrop : MonoBehaviour
{
    public Collider DropTrigger;
    public GameObject DefaultModelDrop;
    public GameObject OilModelDrop;
}

[Serializable]
public class CupDrops
{
    public CupDrop Gumma_buffer_1;
    public CupDrop Gumma_buffer_2;
    public CupDrop Gumma_buffer_3;
    public CupDrop Long_pvp;
    public CupDrop Long_pvp_clear;
    public CupDrop Pvp_clear;
    public CupDrop Long_pvp_2;
}
