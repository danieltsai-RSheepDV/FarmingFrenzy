using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SprayController : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    
    private void Start()
    {
        useAction = PI.FindAction("Use");
        PI.Enable();
    }

    private void Update()
    {
        
    }
}
