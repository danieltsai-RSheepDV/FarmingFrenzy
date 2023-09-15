
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public void OnDeath()
    {
        FindObjectOfType<GameManager>().EnemyDied();
        Destroy(gameObject);
    }
}