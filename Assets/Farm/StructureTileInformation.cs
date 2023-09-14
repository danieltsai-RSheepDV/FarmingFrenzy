using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StructureTileInformation
{
    public string structureId;
    
    public Vector3Int position;
    public int health;
    public bool path;
    public bool destroyed;

    public StructureTileInformation(Vector3Int position)
    {
        this.position = position;
    }
}
