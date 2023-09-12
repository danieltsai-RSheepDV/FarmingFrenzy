using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // public Dictionary<InventoryItemData> inventory = new();

    // public InventoryItem Get(InventoryItemData referenceData){
    //     if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
    //         return value;
    //     }
    //     return null;
    // }
    //
    // public void Add(InventoryItemData referenceData){
    //     if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
    //         value.AddToStack();
    //     }else{
    //         InventoryItem newItem = new InventoryItem(referenceData);
    //         inventory.Add(newItem);
    //         m_itemDictionary.Add(referenceData, newItem);
    //     }
    // }
    //
    // public void Remove(InventoryItemData referenceData){
    //     if(m_itemDictionary.TryGetValue(referenceData, out InventoryItem value)){
    //         value.RemoveFromStack();
    //
    //         if(value.stackSize == 0){
    //             inventory.Remove(value);
    //             m_itemDictionary.Remove(referenceData);
    //         }
    //     }
    // }
}
