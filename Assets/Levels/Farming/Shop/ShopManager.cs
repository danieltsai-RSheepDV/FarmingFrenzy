using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    InventoryManager invManager;

    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
