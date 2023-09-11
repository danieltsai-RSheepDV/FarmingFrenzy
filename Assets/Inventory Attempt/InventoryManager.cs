using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
    enum Item
    {
        Rake,
        Can,
        Pea,
        Potato,
        Fence,
        Path
    }
    Item currTool = Item.Rake;

    public void OnItem1()
    {
        currTool = Item.Rake;
        Debug.Log(currTool);
    }
    public void OnItem2()
    {
        currTool = Item.Can;
        Debug.Log(currTool);
    }
    public void OnItem3()
    {
        currTool = Item.Pea;
        Debug.Log(currTool);
    }
    public void OnItem4()
    {
        currTool = Item.Potato;
        Debug.Log(currTool);
    }
    public void OnItem5()
    {
        currTool = Item.Fence;
        Debug.Log(currTool);
    }

    public void OnItem6()
    {
        currTool = Item.Path;
        Debug.Log(currTool);
    }
}
