using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    // structure of inventory -> inv slots -> (item sprite, amount, number key)
    [SerializeField] Image[] invBgs; // array of background images
    [SerializeField] TextMeshProUGUI[] numKeys; // array of item slot keys
    [SerializeField] Image[] invItems; // array of item images
    // consider extracting info based on structure?

    // color selection
    [SerializeField] Color selectColor; // highlight of tool
    int selectedIndex = 0; // track index of selected tool
    
    InventoryManager invManager;

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        SelectTool(invManager.selectedSlot); // select default tool

        // setup
        // numkeys
        for (int i = 0; i < numKeys.Length; i++)
        {

        }
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
        foreach (Image bg in invBgs)
        {
            bg.color = Color.white;
        }
        invBgs[selectedIndex].color = selectColor; // set the selected image to be selectcolor
    }
}
