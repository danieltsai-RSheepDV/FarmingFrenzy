using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class Clickable : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction mouseAction;
    
    public UnityEvent onClicked = new UnityEvent();
    
    private Camera cam;
    private Collider2D col;
    
    private Vector3 mousePosition = Vector3.zero;
    [NonSerialized] public bool mouseOver = false;

    private void Start()
    {
        cam = Camera.main;
        col = GetComponent<Collider2D>();
        
        useAction = PI.FindAction("Use");
        mouseAction = PI.FindAction("Mouse");
    }

    private void Update()
    {
        mousePosition = cam.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        mousePosition.z = 0;

        mouseOver = col.OverlapPoint(mousePosition);

        if (mouseOver && useAction.triggered)
        {
            onClicked.Invoke();
        }
    }
}