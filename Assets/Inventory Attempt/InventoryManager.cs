using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    public enum Item
    {
        Rake,
        Can,
        Pea,
        Potato,
        Fence,
        Path
    }
    public Item currTool = Item.Rake;
    
    [SerializeField] public InputActionAsset PI;
    private InputAction item1Action;
    private InputAction item2Action;
    private InputAction item3Action;
    private InputAction item4Action;
    private InputAction item5Action;
    private InputAction item6Action;

    private void Start()
    {
        item1Action = PI.FindAction("Item1");
        item2Action = PI.FindAction("Item2");
        item3Action = PI.FindAction("Item3");
        item4Action = PI.FindAction("Item4");
        item5Action = PI.FindAction("Item5");
        item6Action = PI.FindAction("Item6");
    }

    private void Update()
    {
        if (item1Action.triggered)
        {
            currTool = Item.Rake;
        }else if (item2Action.triggered)
        {
            currTool = Item.Can;
        }else if (item3Action.triggered)
        {
            currTool = Item.Pea;
        }else if (item4Action.triggered)
        {
            currTool = Item.Potato;
        }else if (item5Action.triggered)
        {
            currTool = Item.Fence;
        }else if (item6Action.triggered)
        {
            currTool = Item.Path;
        }
    }
}
