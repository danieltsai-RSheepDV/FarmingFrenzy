using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Structure Database")]

public class StructureDatabaseAsset : ScriptableObject
{
    [Serializable]
    public class StructureData
    {
        public string id;
        public string displayName;
        public string itemId;
        public int health;
        public TileBase tile;
    }
    
    public List<StructureData> structures;
}