using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FarmTileInformation
{
    public string cropId;
    
    public Vector3Int position
    {
        get
        {
            return new Vector3Int(Position[0], Position[1], Position[2]);
        }
        set
        {
            Position[0] = value.x;
            Position[1] = value.y;
            Position[2] = value.z;
        }
    }
    public int[] Position = new int[3];
    public int growthStage = 0;
    public bool watered = false;
    public bool tilled = false;
    public bool finished = false;

    public FarmTileInformation(Vector3Int position)
    {
        this.position = position;
    }
}
