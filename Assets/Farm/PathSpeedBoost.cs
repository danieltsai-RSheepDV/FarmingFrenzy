using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpeedBoost : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        PlayerMovement p = col.GetComponent<PlayerMovement>();
        if (p)
        {
            p.onPath = true;
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        PlayerMovement p = col.GetComponent<PlayerMovement>();
        if (p)
        {
            p.onPath = false;
        }
    }
}
