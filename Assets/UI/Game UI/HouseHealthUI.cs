using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseHealthUI : MonoBehaviour
{
    [SerializeField] Slider houseHealth;
    [SerializeField] Health house;
    
    private void Update()
    {
        // display house health
        float currHealth = house.GetHealth() / house.GetMaxHealth();
        houseHealth.value = currHealth;
    }
}
