using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HeldItemController : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction mouseAction;

    private ItemDatabase ItemDatabase;
    [SerializeField] private SpriteRenderer sp;
    private InventoryManager InventoryManager;
    private Camera cam;

    private Vector3 mouseDir;
    
    private void Start()
    {
        ItemDatabase = GameManager.ItemDatabase;
        cam = Camera.main;
        InventoryManager = GetComponentInParent<InventoryManager>();
        
        mouseAction = PI.FindAction("Mouse");
    }

    private void Update()
    {
        //Position
        Vector3 mousePos = cam.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        mousePos.z = 0;
        mouseDir = mousePos - transform.position;
        
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, mouseDir, Vector3.forward));
        sp.flipY = sp.gameObject.transform.position.x > transform.position.x;
        
        //Sprite
        var inventoryItem = InventoryManager.GetSelectedItem();
        sp.sprite = inventoryItem != null ? ItemDatabase[InventoryManager.GetSelectedItem().itemId].icon : null;
    }
}
