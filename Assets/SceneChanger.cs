using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;

    [SerializeField] private TextMeshProUGUI day;
    private int daycont = 1;

    private void Start()
    {
        useAction = PI.FindAction("Use");
    }

    

    
}
