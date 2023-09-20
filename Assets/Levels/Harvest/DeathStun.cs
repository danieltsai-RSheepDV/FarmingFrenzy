using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DeathStun : MonoBehaviour
{
    [SerializeField] private Sprite stun;
    [SerializeField] private Sprite normal;
    [SerializeField] private InputActionAsset pi;
    private PlayerMovement pm;
    private Health health;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
        health = GetComponent<Health>();
    }

    public void Stun()
    {
        pm.allowMovement = false;
        pm.sp.sprite = stun;
        pi.Disable();
        StartCoroutine(Recover());
    }

    IEnumerator Recover()
    {
        yield return new WaitForSeconds(5);
        pm.allowMovement = true;
        pm.sp.sprite = normal;
        health.Heal(1000);
        pi.Enable();
    }
}
