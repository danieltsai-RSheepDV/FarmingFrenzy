using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileInformation : MonoBehaviour
{
    public enum Types
    {
        NONE,
        PEA,
        POTATO
    }

    public Types type;
    
    public Vector3Int position;
    public int growthStage = 0;
    public bool watered = false;
}
