using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    InventoryManager invManager;
    ItemDatabase ItemDatabase;

    const int shopSize = 4;
    [SerializeField] private string[] shopIds = new string[] { "Item_PeaSeeds", "Item_PotatoSeeds", "Item_Fence", "Item_Path" }; // items that are in the shop
    
    [SerializeField] GameObject shopGrid; // the shop's grid
    [SerializeField] GameObject shopItem; // shop item prefab

    // shop item game objects
    public class ShopItemData
    {
        public string itemId;
        public int price;
        public Button buyButton;

        public ShopItemData(string s, int i)
        {
            itemId = s;
            price = i;
        }
    }
    public List<ShopItemData> items = new List<ShopItemData>(); // stores the shop items

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        ItemDatabase = GameManager.ItemDatabase;

        SetUpShop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetUpShop() // show shop items
    {
        for (int i = 0; i < shopIds.Length; i++) // add each item into the shop
        {
            GameObject newShopItem = Instantiate(shopItem); // instantiate shop item

            // create items
            ItemDatabaseAsset.ItemData itemData = ItemDatabase[shopIds[i]]; // get item data
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = itemData.icon; // show shop item sprite
            newShopItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "$" + itemData.price.ToString(); // show item price

            newShopItem.transform.parent = shopGrid.transform; // add shop item to grid
        }
    }

    private void BuyItem(ShopItemData shopItem)
    {
        invManager.DecMoney(shopItem.price);
    }
}
