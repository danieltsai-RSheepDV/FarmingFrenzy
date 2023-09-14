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
    [SerializeField] private StructureManager sm;
    public static StructureManager StructureManager;
    public static ItemDatabase ItemDatabase;

    private int dayCount;

    private void Awake()
    {
        Player = player;
        FarmManager = fm;
        StructureManager = sm;
        ItemDatabase = GetComponent<ItemDatabase>();
    }

    public void ProgressDay()
    {
        FarmManager.UpdateTileMap();
        dayCount++;
    }
    
    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
