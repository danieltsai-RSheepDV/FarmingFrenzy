using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StructureTileInformation
{
    public string structureId;
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
    public int health;
    public bool path;
    public bool destroyed;

    public StructureTileInformation(Vector3Int position)
    {
        this.position = position;
    }
}
