using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemDatabase))]
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    public static GameObject Player;
    [SerializeField] private FarmManager fm;
    public static FarmManager FarmManager;
    public static ItemDatabase ItemDatabase;

    private void Awake()
    {
        Player = player;
        FarmManager = fm;
        ItemDatabase = GetComponent<ItemDatabase>();
    }

    private void ProgressDay()
    {
        FarmManager.UpdateTileMap();
        //     daycont++;
        //     day.text = "Day" + daycont.ToString();
    }
    
    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
