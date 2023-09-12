using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingGameManager : MonoBehaviour
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
}
