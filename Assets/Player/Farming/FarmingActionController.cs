using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FarmingActionController : MonoBehaviour
{
    [SerializeField] private InventoryManager im;
    [SerializeField] private FarmManager fm;

    private void Start()
    {
        if (fm == null) fm = FindObjectOfType<FarmManager>();
    }

    public void UseItem(Vector3Int position)
    {
        if (im.currTool == InventoryManager.Item.Rake)
        {
            fm.TillSoil(position);
            return;
        }

        TileInformation ti;
        if (!fm.GetTile(position, out ti)) return;
        
        if (im.currTool == InventoryManager.Item.Can)
        {
            ti.watered = true;
            fm.UpdateTileMap(position);
            return;
        }

        if (!String.IsNullOrEmpty(ti.cropId)) return;
        
        switch (im.currTool)
        {
            case InventoryManager.Item.Pea:
                ti.cropId = "Crop_Potato";
                fm.UpdateTileMap(position);
                break;
            case InventoryManager.Item.Potato:
                ti.cropId = "Crop_Pea";
                fm.UpdateTileMap(position);
                break;
        }
    }
}
