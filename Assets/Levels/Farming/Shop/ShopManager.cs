using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // argument null exceptions when game object selected:
    // https://forum.unity.com/threads/argumentnullexception-value-cannot-be-null-parameter-name-_unity_self.1431901/
    InventoryManager invManager;
    ItemDatabase ItemDatabase;

    const int toolsSkipped = 2; // number of tools before consumables
    const string moneySymb = "$";
    [SerializeField] private string[] shopIds = new string[] { "Item_PeaSeeds", "Item_PotatoSeeds", "Item_Fence", "Item_Path" }; // items that are in the shop
    
    [SerializeField] GameObject shopGrid; // the shop's grid
    [SerializeField] GameObject shopItem; // shop item prefab

    // shop item game objects
    public class ShopItemData
    {
        public string itemId;
        public int index;
        public float price;
        public Button buyButton;

        public ShopItemData(string s, int i, float p)
        {
            itemId = s;
            index = i;
            price = p;
        }
    }
    public List<ShopItemData> shopItems = new List<ShopItemData>(); // stores the shop items

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        ItemDatabase = GameManager.ItemDatabase;

        SetUpShop();
    }

    private void SetUpShop() // show shop items
    {
        for (int i = 0; i < shopIds.Length; i++) // add each item into the shop
        {
            GameObject newShopItem = Instantiate(shopItem); // instantiate shop item

            // create items
            string id = shopIds[i]; // item id
            ItemDatabaseAsset.ItemData itemData = ItemDatabase[id]; // get item data
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = itemData.icon; // show shop item sprite
            float price = itemData.price;
            newShopItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = moneySymb + price.ToString(); // show item price

            newShopItem.transform.SetParent(shopGrid.transform); // add shop item to grid

            // add items to list
            ShopItemData newStoredShopItem = new ShopItemData(id, i, price); // new ShopItem instantiated

            newStoredShopItem.buyButton = newShopItem.transform.GetChild(3).GetComponent<Button>(); // store button
            newStoredShopItem.buyButton.onClick.AddListener(delegate { BuyItem(newStoredShopItem); }); // trigger buying of item on click

            shopItems.Add(newStoredShopItem); // add item to list
        }
    }

    private void BuyItem(ShopItemData shopItem)
    { 
        invManager.DecMoney(shopItem.price);
        invManager.items[shopItem.index + toolsSkipped].amount++; // increase amount
    }
}
