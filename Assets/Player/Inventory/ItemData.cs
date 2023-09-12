using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory Item Data")]

public class ItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
}
