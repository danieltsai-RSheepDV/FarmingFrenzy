using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InventoryManager))]
public class FarmingActionController : MonoBehaviour
{
    private const string WATERING_CAN = "Item_WateringCan";
    private const string RAKE = "Item_Rake";
    
    private FarmManager farmManager;
    private ItemDatabase itemDatabase;
    private InventoryManager inventoryManager;

    private void Start()
    {
        farmManager = GameManager.FarmManager;
        inventoryManager = GetComponent<InventoryManager>();
        itemDatabase = GameManager.ItemDatabase;
    }

    public void UseItem(Vector3Int position)
    {
        try
        {
            if (inventoryManager.GetSelectedItem() == default)
            {
                Debug.Log("Attempted to use nothing! Not an error, just kinda weird.");
                return;
            }
            
            ItemDatabaseAsset.ItemData itemData = itemDatabase[inventoryManager.GetSelectedItem().itemId];
            if (itemData.tags.Contains("seed"))
            {
                if(farmManager.PlantSeed(itemData.id, position)) inventoryManager.RemoveItem(itemData.id);
            }else if (itemData.id == WATERING_CAN)
            {
                farmManager.WaterSoil(position);
            }else if (itemData.id == RAKE)
            {
                farmManager.TillSoil(position);
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e);
        }
    }
}
