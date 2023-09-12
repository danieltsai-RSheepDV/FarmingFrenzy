using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public static GameObject Player;
    [SerializeField] private FarmManager fm;
    public static FarmManager FarmManager;

    private void Awake()
    {
        Player = player;
        FarmManager = fm;
    }

    private void ProgressDay()
    {
        // if (useAction.WasPressedThisFrame() && collision.gameObject == GameManager.Player)
        // {
        //     GameManager.FarmManager.UpdateTileMap();
        //     daycont++;
        //     day.text = "Day" + daycont.ToString();
        // }
    }
}
