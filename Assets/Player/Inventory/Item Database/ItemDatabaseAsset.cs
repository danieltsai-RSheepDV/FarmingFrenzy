using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Database")]

public class ItemDatabaseAsset : ScriptableObject
{
    [Serializable]
    public class ItemData
    {
        public string id;
        public string displayName;
        public Sprite icon;
    }
    
    public List<ItemData> items;
}