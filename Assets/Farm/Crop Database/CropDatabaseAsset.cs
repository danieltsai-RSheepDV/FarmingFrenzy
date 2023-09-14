using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Crop Database")]

public class CropDatabaseAsset : ScriptableObject
{
    [Serializable]
    public class CropData
    {
        public string id;
        public string displayName;
        public string seedId;
        public int growthTime;
        public GameObject monsterPrefab;
        public List<CropTileData> tiles;
    }

    [Serializable]
    public struct CropTileData
    {
        public TileBase tile;
        public int growthStage;
    }
    
    public List<CropData> crops;
}