
using System.Collections.Generic;
using UnityEngine;

public class CropDatabase : MonoBehaviour
{
    [SerializeField] private CropDatabaseAsset asset;
    
    private Dictionary<string, CropDatabaseAsset.CropData> cache = new();
    private Dictionary<string, string> seedCache = new();

    public CropDatabaseAsset.CropData this[string key]
    {
        get
        {
            if (cache.ContainsKey(key))
            {
                return cache[key];
            }
            else
            {
                CropDatabaseAsset.CropData searchResult = asset.items.Find(item => item.id == key);
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
            CropDatabaseAsset.CropData searchResult = asset.items.Find(item => item.id == key);
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

    public string ToCropId(string id)
    {
        if (seedCache.ContainsKey(id)) return seedCache[id];

        CropDatabaseAsset.CropData searchResult = asset.items.Find(item => item.seedId == id);
        string cropId = searchResult?.id;
        seedCache.Add(id, cropId);
        
        if(cropId == null) Debug.Log("Associated Crop Doesn't Exist!: " + id);
        
        return cropId;
    }
}