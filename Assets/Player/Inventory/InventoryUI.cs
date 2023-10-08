using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using static UnityEditor.Progress;
using static InventoryManager;

public class InventoryUI : MonoBehaviour
{
    InventoryManager invManager;
    ItemDatabase ItemDatabase;

    // color selection
    [SerializeField] Color selectColor; // highlight of tool
    int selectedIndex = 0; // track index of selected tool

    // structure of inventory -> inv slots -> (item sprite, amount, number key)
    [SerializeField] GameObject invSlot;

    const int INVENTORY_SLOTS = 6;
    [SerializeField] Transform invBar;
    // inv slot ui stored in here

    # region InvSlotUi Class
    public class InvSlotUi
    {
        Image itemBg;
        Image itemImg;
        TextMeshProUGUI amount;

        const int offset = 14;

        public InvSlotUi(Image bg, Image s, TextMeshProUGUI a)
        {
            itemBg = bg;
            itemImg = s;
            amount = a;
        }

        public void SetBackground(Color color)
        {
            itemBg.color = color;
        }

        public void SetSprite(Sprite s)
        {
            itemImg.enabled = true;
            itemImg.sprite = s;
        }

        public void SetAmount(float a, bool visible = true) // if amount is visible, set to amount
        {
            amount.enabled = visible;
            if (visible)
            {
                amount.text = a.ToString();
                amount.enabled = true;
                itemImg.transform.localPosition = new Vector3(offset, 0, 0); // offset to the side to accommodate amount 
            } else
            {
                amount.enabled = false; // disable amount
                itemImg.transform.localPosition = Vector3.zero; // center if invisible
            }
            
        }

        public void Disable()
        {
            itemImg.enabled = false; // disable image component
            amount.enabled = false; // disable amount
        }
    }
    #endregion
    InvSlotUi[] inventorySlots = new InvSlotUi[INVENTORY_SLOTS]; // store itemSlot game objects

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        ItemDatabase = GameManager.ItemDatabase;

        // inventory slot instantiation
        for (int i = 0; i < INVENTORY_SLOTS; i++)
        {
            GameObject newInvSlot = Instantiate(invSlot, invBar); // instantiate and set its parent to inventory bar
            
            InvSlotUi newUi = new InvSlotUi(
                newInvSlot.GetComponent<Image>(), // background image
                newInvSlot.transform.GetChild(0).GetComponent<Image>(), // image sprite
                newInvSlot.transform.GetChild(1).GetComponent<TextMeshProUGUI>() // amount text
            ); // add inv slot to array
            inventorySlots[i] = newUi;
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
        for (int i = 0; i < INVENTORY_SLOTS; i++)
        {
            InventorySlotData itemStack = invManager.items[i]; // get item
            InvSlotUi currInvSlot = inventorySlots[i]; // access this item slot

            if (itemStack != null) // if item is not null
            {
                ItemDatabaseAsset.ItemData itemData = ItemDatabase[itemStack.itemId]; // get item data
                currInvSlot.SetSprite(itemData.icon); // show item sprite

                // show item amount if max is not 1
                if (itemData.maxStack != 1)
                {
                    currInvSlot.SetAmount(itemStack.amount); // show amount
                } else // max is 1, hide amount
                {
                    currInvSlot.SetAmount(0, false);
                }
            }
            else
            {
                // item is null
                currInvSlot.Disable();
            }
        }
    }

    private void SelectTool(int index)
    {
        selectedIndex = index;

        // set all bg images to white
        for (int i = 0; i < INVENTORY_SLOTS; i++)
        {
            inventorySlots[i].SetBackground(Color.white); // set background to baseline
        }
        inventorySlots[selectedIndex].SetBackground(selectColor); // set the selected image to be selectcolor
    }
}
