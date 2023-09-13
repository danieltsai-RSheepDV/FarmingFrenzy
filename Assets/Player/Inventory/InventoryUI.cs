using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] Image[] invItems; // array of background images

    // color selection
    [SerializeField] Color selectColor; // highlight of tool
    int selectedIndex = 0; // track index of selected tool
    
    InventoryManager invManager;

    void Awake()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        SelectTool(invManager.selectedSlot);
    }

    void Update()
    {
        if (invManager.selectedSlot != selectedIndex) // if tool has been changed
        {
            SelectTool(invManager.selectedSlot); // select the new tool
        }   
    }

    private void SelectTool(int index)
    {
        selectedIndex = index;

        // set all bg images to white
        foreach (Image bg in invItems)
        {
            bg.color = Color.white;
        }
        invItems[selectedIndex].color = selectColor; // set the selected image to be selectcolor
    }
}
