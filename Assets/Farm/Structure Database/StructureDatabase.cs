
using System.Collections.Generic;
using UnityEngine;

public class StructureDatabase : MonoBehaviour
{
    [SerializeField] private StructureDatabaseAsset asset;
    
    private Dictionary<string, StructureDatabaseAsset.StructureData> cache = new();
    private Dictionary<string, string> itemCache = new();

    public StructureDatabaseAsset.StructureData this[string key]
    {
        get
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            else
            {
                StructureDatabaseAsset.StructureData searchResult = asset.structures.Find(item => item.id == key);
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
            StructureDatabaseAsset.StructureData searchResult = asset.structures.Find(item => item.id == key);
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

    public string ToItemId(string id)
    {
        if (itemCache.ContainsKey(id)) return itemCache[id];

        StructureDatabaseAsset.StructureData searchResult = asset.structures.Find(item => item.itemId == id);
        string cropId = searchResult?.id;
        itemCache.Add(id, cropId);
        
        if(cropId == null) Debug.Log("Associated Structure Doesn't Exist!: " + id);
        
        return cropId;
    }
}