
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabase : MonoBehaviour
{
    [SerializeField] private ItemDatabaseAsset asset;
    
    private Dictionary<string, ItemDatabaseAsset.ItemData> cache = new();

    public ItemDatabaseAsset.ItemData this[string key]
    {
        get
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            else
            {
                ItemDatabaseAsset.ItemData searchResult = asset.items.Find(item => item.id == key);
                if (searchResult != null)
                {
                    cache.Add(key, searchResult);
                    return searchResult;
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }
    }

    public bool HasKey(string key)
    {
        if (cache.ContainsKey(key))
        {
            return true;
        }
        else
        {
            ItemDatabaseAsset.ItemData searchResult = asset.items.Find(item => item.id == key);
            if (searchResult != null)
            {
                cache.Add(key, searchResult);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}