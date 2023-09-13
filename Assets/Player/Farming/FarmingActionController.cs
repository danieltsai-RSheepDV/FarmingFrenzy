using System;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class FarmingActionController : MonoBehaviour
{
    private FarmManager fm;
    private InventoryManager im;

    private void Start()
    {
        fm = GameManager.FarmManager;
        im.GetComponent<InventoryManager>();
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
