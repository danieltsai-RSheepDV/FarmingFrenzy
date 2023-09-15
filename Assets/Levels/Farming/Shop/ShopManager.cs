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

    [SerializeField] private string[] shopIds = new string[] { "Item_PeaSeeds", "Item_PotatoSeeds", "Item_Fence", "Item_Path" }; // items that are in the shop
    [SerializeField] GameObject shopGrid; // the shop's grid
    [SerializeField] GameObject shopItem; // shop item prefab

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        ItemDatabase = GameManager.ItemDatabase;

        SetUpShop();
    }

    private void SetUpShop() // show shop items
    {
        const string moneySymb = "$";
        for (int i = 0; i < shopIds.Length; i++) // add each item into the shop
        {
            // instantiate shop item
            GameObject newShopItem = Instantiate(shopItem);

            // create items
            string id = shopIds[i]; // store item id
            ItemDatabaseAsset.ItemData itemData = ItemDatabase[id]; // get item data

            // show item sprite
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = itemData.icon;

            // show item price
            float price = itemData.price;
            newShopItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = moneySymb + price.ToString();

            // hook up button
            Button buyButton = newShopItem.transform.GetChild(3).GetComponent<Button>(); // store button
            buyButton.onClick.AddListener(delegate { BuyItem(id, price); }); // trigger buying of item on click

            // add shop item to grid
            newShopItem.transform.SetParent(shopGrid.transform);
        }
    }

    private void BuyItem(string itemId, float price)
    {
        invManager.DecMoney(price); // decrease money
        invManager.AddItem(itemId); // increase corresponding amount
    }
}
