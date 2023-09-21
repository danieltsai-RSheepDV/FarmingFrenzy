using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ItemDatabase))]
public class GameManager : MonoBehaviour
{
    private SaveVersion _saveVersion;
    
    [SerializeField] private GameObject player;
    public static GameObject Player;
    [SerializeField] private GameObject house;
    public static GameObject House;
    [SerializeField] private FarmManager fm;
    public static FarmManager FarmManager;
    [SerializeField] private StructureManager sm;
    public static StructureManager StructureManager;
    public static ItemDatabase ItemDatabase;
    
    // sound manager
    [SerializeField] private SoundManager soundM;
    public static SoundManager SoundManager;

    [SerializeField] private HarvestStatsUI HarvestStatsUI;
    [SerializeField] private Animator fader;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private bool summonMonsters;

    private int dayCount = 1;

    private void Awake()
    {
        _saveVersion = GetComponent<SaveVersion>();
        
        Player = player;
        FarmManager = fm;
        StructureManager = sm;
        House = house;
        ItemDatabase = GetComponent<ItemDatabase>();

        SoundManager = soundM; // sound manager
    }

    private void Start()
    {
        SaveData s =  SaveManager.Load(_saveVersion.saveVersion);
        if (s != null)
        {
            FarmManager.tiles = s.farmTiles;
            StructureManager.tiles = s.structureTiles;
            dayCount = s.dayCount;
            Player.GetComponent<InventoryManager>().items = s.playerItems;
            Player.GetComponent<InventoryManager>().money = s.money;
            
            FarmManager.LoadTileMap();
            StructureManager.UpdateTileMap();

            if (summonMonsters)
            {
                int earnings;
                HarvestStatsUI.numEnemies = FarmManager.SummonMonsters(out earnings);
                HarvestStatsUI.earnings = earnings;
            }
        }
        day.text = "Day " + dayCount;
        
    }

    public void EnemyDied()
    {
        HarvestStatsUI.enemyCounter++;
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
        
        if (FarmManager.shouldSummonMonsters)
        {
            SaveManager.SaveBackup(_saveVersion.saveVersion);
        }
        
        SaveManager.Save(_saveVersion.saveVersion, FarmManager, StructureManager, player.GetComponent<InventoryManager>(), dayCount);
        SceneManager.LoadScene(FarmManager.shouldSummonMonsters ? "HarvestScene" : "FarmingScene");
    }
    
    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    public void Revert()
    {
        SaveManager.RevertToBackup(_saveVersion.saveVersion);
    }
}
