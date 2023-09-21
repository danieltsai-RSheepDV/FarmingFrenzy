using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    // sfx
    [SerializeField] SoundManager SoundManager;
    
    
    // argument null exceptions when game object selected:
    // https://forum.unity.com/threads/argumentnullexception-value-cannot-be-null-parameter-name-_unity_self.1431901/
    InventoryManager invManager;
    ItemDatabase ItemDatabase;

    private Canvas canvas;

    [SerializeField] private string[] shopIds = new string[] { "Item_PeaSeeds", "Item_PotatoSeeds", "Item_Fence", "Item_Path" }; // items that are in the shop
    [SerializeField] GameObject shopItem; // shop item prefab
    [SerializeField] Transform shopGrid; // the shop's grid

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
        ItemDatabase = GameManager.ItemDatabase;

        canvas = GetComponent<Canvas>();

        SetUpShop();
        ToggleShop(false);
    }

    private void SetUpShop() // show shop items
    {
        const string moneySymb = "$";
        for (int i = 0; i < shopIds.Length; i++) // add each item into the shop
        {
            // instantiate shop item and set parent to grid
            GameObject newShopItem = Instantiate(shopItem, shopGrid);

            // create items
            string id = shopIds[i]; // store item id
            ItemDatabaseAsset.ItemData itemData = ItemDatabase[id]; // get item data

            // show item sprite
            newShopItem.transform.GetChild(0).GetComponent<Image>().sprite = itemData.icon;

            // show item price
            float price = itemData.price;
            newShopItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = moneySymb + price.ToString();

            // show item description
            string description = itemData.desc;
            newShopItem.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = description;

            // hook up button
            Button buyButton = newShopItem.transform.GetChild(3).GetComponent<Button>(); // store button
            buyButton.onClick.AddListener(delegate { BuyItem(id, price); }); // trigger buying of item on click
        }
    }

    private void BuyItem(string itemId, float price)
    {
        if (invManager.DecMoney(price))
        {
            invManager.AddItem(itemId); // increase corresponding amount
        }
        
    }
    
    public void ToggleShop(bool b)
    {
        canvas.enabled = b;
        if (b) SoundManager.PlayUIOpenSound(); // if opening shop
    }
}
