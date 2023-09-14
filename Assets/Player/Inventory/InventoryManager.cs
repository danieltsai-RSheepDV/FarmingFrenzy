using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    const int INVENTORY_SLOTS = 6;
    
    [SerializeField] public InputActionAsset PI;
    private InputAction item1Action;
    private InputAction item2Action;
    private InputAction item3Action;
    private InputAction item4Action;
    private InputAction item5Action;
    private InputAction item6Action;


    public (string itemId, int amount)[] items = new (string itemId, int amount)[INVENTORY_SLOTS];
    public int selectedSlot = 0;
    
    private ItemDatabase ItemDatabase;

    // for money
    private float money;

    private void Start()
    {
        ItemDatabase = GameManager.ItemDatabase;
        
        item1Action = PI.FindAction("Item1");
        item2Action = PI.FindAction("Item2");
        item3Action = PI.FindAction("Item3");
        item4Action = PI.FindAction("Item4");
        item5Action = PI.FindAction("Item5");
        item6Action = PI.FindAction("Item6");

        AddItem("Item_Rake");
        AddItem("Item_WateringCan");
        AddItem("Item_PeaSeeds");
        AddItem("Item_PeaSeeds");
        AddItem("Item_PeaSeeds");
        AddItem("Item_PotatoSeeds");
        AddItem("Item_PotatoSeeds");
        AddItem("Item_PotatoSeeds");
        AddItem("Item_PotatoSeeds");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Fence");
        AddItem("Item_Path");
        AddItem("Item_Path");
        AddItem("Item_Path");
        AddItem("Item_Path");
        AddItem("Item_Path");
        AddItem("Item_Path");
        AddItem("Item_Path");
    }

    private void Update()
    {
        if (item1Action.triggered)
        {
            selectedSlot = 0;
        }else if (item2Action.triggered)
        {
            selectedSlot = 1;
        }else if (item3Action.triggered)
        {
            selectedSlot = 2;
        }else if (item4Action.triggered)
        {
            selectedSlot = 3;
        }else if (item5Action.triggered)
        {
            selectedSlot = 4;
        }else if (item6Action.triggered)
        {
            selectedSlot = 5;
        }
    }

    public (string itemId, int amount) GetSelectedItem()
    {
        return items[selectedSlot];
    }

    public bool AddItem(string id)
    {
        if (ItemDatabase.HasKey(id))
        {
            ItemDatabaseAsset.ItemData itemData = ItemDatabase[id];
            int inventoryIndex = GetItemIndex(id);
            
            if (inventoryIndex >= 0)
            {
                if(items[inventoryIndex].amount < itemData.maxStack) items[inventoryIndex].amount++;
                return true;
            }
            else
            {
                inventoryIndex = FindFirstEmpty();
                if (inventoryIndex < 0) return false;
                
                items[inventoryIndex].itemId = id;
                items[inventoryIndex].amount = 1;
                return true;
            }
        }
        else
        {
            Debug.LogError("You attempted to add an item that doesn't exist! : " + id);
            return false;
        }
    }

    public bool RemoveItem(string id)
    {
        if (ItemDatabase.HasKey(id))
        {
            int inventoryIndex = GetItemIndex(id);
            
            if (inventoryIndex >= 0)
            {
                items[inventoryIndex].amount--;
                if (items[inventoryIndex].amount <= 0) items[inventoryIndex] = default;
                
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            Debug.LogError("You attempted to remove an item that doesn't exist! : " + id);
            return false;
        }
    }

    public int FindFirstEmpty()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].Equals(default)) return i;
        }

        return -1;
    }

    public int GetItemIndex(string id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemId == id) return i;
        }

        return -1;
    }

    // money stuff
    public float GetMoney()
    {
        return money;
    }

    public bool SetMoney(float amount)
    {
        if(amount <= money) // no debt check!
        {
            money -= amount;
            return true;
        } else
        {
            return false;
        }
    }
}
