using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FarmTileInformation
{
    public string cropId;
    
    public Vector3Int position;
    public int growthStage = 0;
    public bool watered = false;
    public bool tilled = false;
    public bool finished = false;

    public FarmTileInformation(Vector3Int position)
    {
        this.position = position;
    }
}
