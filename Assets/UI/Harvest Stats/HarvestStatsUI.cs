using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestStatsUI : MonoBehaviour
{
    private Animator Animator;

    public int numEnemies;

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
                ToggleStats(true);
                GameManager.Player.GetComponent<PlayerMovement>().allowMovement = false;
            }
        }
    }
    private int EnemyCounter;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        
        ToggleStats(false);
    }

    public void ToggleStats(bool b)
    {
        Animator.SetBool("Visible", b);
    }
}
