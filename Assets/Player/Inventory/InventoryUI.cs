using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static UnityEditor.Progress;
using static InventoryManager;

public class InventoryUI : MonoBehaviour
{
    // structure of inventory -> inv slots -> (item sprite, amount, number key)
    [SerializeField] Image[] invBgs; // array of background images
    [SerializeField] Image[] invItems; // array of item images
    [SerializeField] TextMeshProUGUI[] invAmts; // array of item amounts
    [SerializeField] TextMeshProUGUI[] invNumKeys; // array of item slot keys
    // consider extracting info based on structure?

    // color selection
    [SerializeField] Color selectColor; // highlight of tool
    int selectedIndex = 0; // track index of selected tool
    
    InventoryManager invManager;
    ItemDatabase ItemDatabase;

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        ItemDatabase = GameManager.ItemDatabase;

        // inventory setup
        // numkeys (supports diff sized inventories)
        for (int i = 0; i < invNumKeys.Length; i++)
        {
            invNumKeys[i].text = (i + 1).ToString();
        }
        SelectTool(invManager.selectedSlot); // select default tool
    }

    void Update()
    {
        if (invManager.selectedSlot != selectedIndex) // if tool has been changed
        {
            SelectTool(invManager.selectedSlot); // select the new tool
        }

        // invItems sprites and amounts (updated every frame O.O)
        for (int i = 0; i < invItems.Length; i++)
        {
            InventorySlotData item = invManager.items[i]; // get item
            if (item != null) // if item is not null
            {
                ItemDatabaseAsset.ItemData itemData = ItemDatabase[item.itemId]; // get item data
                invItems[i].sprite = itemData.icon; // show item sprite
                
                // show item amount if max is not 1
                if(itemData.maxStack != 1)
                {
                    invAmts[i].enabled = true; // enable the text component
                    invAmts[i].text = item.amount.ToString(); // convert number to string
                }
            } else // item is null
            {
                invItems[i].enabled = false; // disable image component
                invAmts[i].enabled = false; // diable item amount
            }
        }
    }

    private void SelectTool(int index)
    {
        selectedIndex = index;

        // set all bg images to white
        foreach (Image bg in invBgs)
        {
            bg.color = Color.white;
        }
        invBgs[selectedIndex].color = selectColor; // set the selected image to be selectcolor
    }
}
