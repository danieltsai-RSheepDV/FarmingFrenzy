using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Slider houseHealth;
    [SerializeField] Health house;

    [SerializeField] GameObject[] invItems;
    [SerializeField] InventoryManager invManager;

    private void Update()
    {
        // display house health
        float currHealth = house.GetHealth() / house.GetMaxHealth();
        houseHealth.value = currHealth;

        InventoryManager.Item tool = invManager.currTool;
        switch (tool)
        {
            case InventoryManager.Item.Rake:
                
                break;
            case InventoryManager.Item.Can:
                break;
            case InventoryManager.Item.Pea:
                break;
            case InventoryManager.Item.Potato:
                break;
            case InventoryManager.Item.Fence:
                break;
            case InventoryManager.Item.Path:
                break;
            default:
                break;
        }
    }
}
