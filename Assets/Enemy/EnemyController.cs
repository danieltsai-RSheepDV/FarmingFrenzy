
using System;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public void OnDeath()
    {
        FindObjectOfType<GameManager>().EnemyDied();
        Destroy(gameObject);
    }

    public abstract void Initialize();
}