using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemDatabase))]
public class GameManager : MonoBehaviour
{
    private float saveVersion;
    private SaveVersion _saveVersion;
    
    [SerializeField] private GameObject player;
    public static GameObject Player;
    [SerializeField] private FarmManager fm;
    public static FarmManager FarmManager;
    [SerializeField] private StructureManager sm;
    public static StructureManager StructureManager;
    public static ItemDatabase ItemDatabase;

    [SerializeField] private Animator fader;
    [SerializeField] private bool summonMonsters;

    private int dayCount = 0;

    private void Awake()
    {
        _saveVersion = GetComponent<SaveVersion>();
        
        Player = player;
        FarmManager = fm;
        StructureManager = sm;
        ItemDatabase = GetComponent<ItemDatabase>();
    }

    private void Start()
    {
        saveVersion = _saveVersion.saveVersion;
        
        SaveData s = SaveManager.Load(saveVersion);
        if (s != null)
        {
            FarmManager.tiles = s.farmTiles;
            StructureManager.tiles = s.structureTiles;
            dayCount = s.dayCount;
            
            FarmManager.LoadTileMap();
            StructureManager.UpdateTileMap();

            if (summonMonsters)
            {
                FarmManager.SummonMonsters();
                FarmManager.shouldSummonMonsters = false;
            }
            else
            {
                Player.GetComponent<InventoryManager>().items = s.playerItems;
            }
        }
        
    }

    public void ProgressDay()
    {
        fader.SetTrigger("FadeOut");
    }

    public void DayUpdate()
    {
        dayCount++;
        FarmManager.UpdateTileMap();
        StructureManager.UpdateTileMap();
        SaveManager.Save(saveVersion, FarmManager, StructureManager, player.GetComponent<InventoryManager>(), dayCount);
        SceneManager.LoadScene(FarmManager.shouldSummonMonsters ? "HarvestScene" : "FarmingScene");
    }
    
    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
