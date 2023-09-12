using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] public FarmManager fm;
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;

    [SerializeField] private TextMeshProUGUI day;
    private int daycont = 1;

    private void Start()
    {
        useAction = PI.FindAction("Use");
    }

    public void ChangeScene(string s)
    {
        SceneManager.LoadScene(s);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (useAction.WasPressedThisFrame() && collision.gameObject == FarmingGameManager.Player)
        {
            fm.UpdateTileMap();
            daycont++;
            day.text = "Day" + daycont.ToString();
        }
    }
}
