using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] Slider houseHealth;
    [SerializeField] Health house;

    [SerializeField] Image[] invItems; // array of background images
    [SerializeField] InventoryManager invManager;

    // 4A3C43

    private void Update()
    {
        // display house health
        float currHealth = house.GetHealth() / house.GetMaxHealth();
        houseHealth.value = currHealth;

        InventoryManager.Item tool = invManager.currTool;
        switch (tool)
        {
            case InventoryManager.Item.Rake:
                invItems[0].color = Color.red;
                invItems[1].color = Color.white;
                invItems[2].color = Color.white;
                invItems[3].color = Color.white;
                invItems[4].color = Color.white;
                invItems[5].color = Color.white;
                break;
            case InventoryManager.Item.Can:
                invItems[0].color = Color.white;
                invItems[1].color = Color.red;
                invItems[2].color = Color.white;
                invItems[3].color = Color.white;
                invItems[4].color = Color.white;
                invItems[5].color = Color.white;
                break;
            case InventoryManager.Item.Pea:
                invItems[0].color = Color.white;
                invItems[1].color = Color.white;
                invItems[2].color = Color.red;
                invItems[3].color = Color.white;
                invItems[4].color = Color.white;
                invItems[5].color = Color.white;
                break;
            case InventoryManager.Item.Potato:
                invItems[0].color = Color.white;
                invItems[1].color = Color.white;
                invItems[2].color = Color.white;
                invItems[3].color = Color.red;
                invItems[4].color = Color.white;
                invItems[5].color = Color.white;
                break;
            case InventoryManager.Item.Fence:
                invItems[0].color = Color.white;
                invItems[1].color = Color.white;
                invItems[2].color = Color.white;
                invItems[3].color = Color.white;
                invItems[4].color = Color.red;
                invItems[5].color = Color.white;
                break;
            case InventoryManager.Item.Path:
                invItems[0].color = Color.white;
                invItems[1].color = Color.white;
                invItems[2].color = Color.white;
                invItems[3].color = Color.white;
                invItems[4].color = Color.white;
                invItems[5].color = Color.red;
                break;
            default:
                break;
        }
    }
}
