using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileInformation
{
    public enum Types
    {
        NONE,
        PEA,
        POTATO
    }

    public Types type = Types.NONE;
    
    public Vector3Int position;
    public int growthStage = 0;
    public bool watered = false;
    public bool tilled = false;
    public bool finished = false;

    public TileInformation(Vector3Int position)
    {
        this.position = position;
    }
}
