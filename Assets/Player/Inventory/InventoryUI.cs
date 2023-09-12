using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Image[] invItems; // array of background images
    [SerializeField] InventoryManager invManager;

    // color selection
    [SerializeField] Color selectColor; // highlight of tool
    InventoryManager.Item selectedTool; // track currently selected tool on toolbar
    int selectedIndex = 0; // track index of selected tool

    void Awake()
    {
        if(!invManager) invManager = FindObjectOfType<InventoryManager>(); // find the inventory manager
        SelectTool(InventoryManager.Item.Rake); // start on Rake
    }

    void Update()
    {
        InventoryManager.Item currTool = invManager.currTool; // get current tool
        if (selectedTool != currTool) // if tool has been changed
        {
            SelectTool(currTool); // select the new tool
        }   
    }

    private void SelectTool(InventoryManager.Item tool)
    {
        selectedTool = tool;
        switch (tool) // get the index of the selected tool
        {
            case InventoryManager.Item.Rake:
                selectedIndex = 0;
                break;
            case InventoryManager.Item.Can:
                selectedIndex = 1;
                break;
            case InventoryManager.Item.Pea:
                selectedIndex = 2;
                break;
            case InventoryManager.Item.Potato:
                selectedIndex = 3;
                break;
            case InventoryManager.Item.Fence:
                selectedIndex = 4;
                break;
            case InventoryManager.Item.Path:
                selectedIndex = 5;
                break;
            default:
                break;
        }

        // set all bg images to white
        foreach (Image bg in invItems)
        {
            bg.color = Color.white;
        }
        invItems[selectedIndex].color = selectColor; // set the selected image to be selectcolor
    }
}
