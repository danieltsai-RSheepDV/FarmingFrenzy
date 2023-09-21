using System;
using TMPro;
using UnityEngine;

public class HarvestStatsUI : MonoBehaviour
{
    private Animator Animator;

    [NonSerialized] public int numEnemies;
    [NonSerialized] public int earnings;

    private int modifiedEarnings;

    [SerializeField] private TextMeshProUGUI earningTextUI;

    //sfx
    SoundManager SoundManager;

    public int enemyCounter
    {
        get
        {
            return EnemyCounter;
        }
        set
        {
            EnemyCounter = value;
            if (EnemyCounter >= numEnemies)
            {
                GameManager.Player.GetComponent<PlayerMovement>().allowMovement = false;
                modifiedEarnings = Mathf.CeilToInt(earnings * (1 + Mathf.Log(numEnemies, 20)));
                GameManager.Player.GetComponent<InventoryManager>().IncMoney(modifiedEarnings);
                
                ToggleStats(true);
            }
        }
    }
    private int EnemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        SoundManager = GameObject.FindGameObjectWithTag("Sound").GetComponent<SoundManager>();
        
        Animator = GetComponent<Animator>();
        
        ToggleStats(false);
    }

    public void ToggleStats(bool b)
    {
        Animator.SetBool("Visible", b);
        
        float multiplier = (1 + Mathf.Log(numEnemies, 20));
        multiplier = Mathf.Round(multiplier * 100) / 100f;
        
        earningTextUI.text = "You earned $" + modifiedEarnings + "!\n" +
                             multiplier + "x multiplier!";

        if (b) SoundManager.PlaySuccessSound();
    }
}
