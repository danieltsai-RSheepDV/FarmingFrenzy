using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverlayManager : MonoBehaviour
{
    InventoryManager invManager;
    [SerializeField] TextMeshProUGUI moneyText;

    const string moneySymb = "$";
    void Start()
    {
        invManager = GameManager.Player.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = moneySymb + invManager.GetMoney();
    }
}
