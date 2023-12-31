﻿using System;
using System.Collections.Generic;
using JetBrains.Annotations;
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
        
        public float price;
        public string desc;

        public float timeToUse;
        
        public int maxStack = Int32.MaxValue;
        public List<string> tags;
    }
    
    public List<ItemData> items;
}