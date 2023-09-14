
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData
{
    public List<FarmTileInformation> farmTiles;
    public List<StructureTileInformation> structureTiles;
    public InventoryManager.InventorySlotData[] playerItems;
    public int dayCount;

    public SaveData(FarmManager farmManager, StructureManager structureManager, InventoryManager inventoryManager,
        int dayCount)
    {
        farmTiles = farmManager.tiles;
        structureTiles = structureManager.tiles;
        playerItems = inventoryManager.items;
        this.dayCount = dayCount;
    }
}