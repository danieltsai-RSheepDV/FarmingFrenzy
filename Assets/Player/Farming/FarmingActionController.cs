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

        if (ti.type != TileInformation.Types.NONE) return;
        
        switch (im.currTool)
        {
            case InventoryManager.Item.Pea:
                ti.type = TileInformation.Types.PEA;
                fm.UpdateTileMap(position);
                break;
            case InventoryManager.Item.Potato:
                ti.type = TileInformation.Types.POTATO;
                fm.UpdateTileMap(position);
                break;
        }
    }
}
