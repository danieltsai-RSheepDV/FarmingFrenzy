using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseHealthUI : MonoBehaviour
{
    [SerializeField] Health house;
    
    private Slider slider;

    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {
        // display house health
        float currHealth = house.GetHealth() / house.GetMaxHealth();
        slider.value = currHealth;
    }
}
