using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(CropDatabase))]
public class FarmManager : MonoBehaviour
{
    [SerializeField] private Tilemap dirt;
    [SerializeField] private Tilemap crops;
    [SerializeField] private TileBase untilledTile;
    [SerializeField] private TileBase tilledTile;
    [SerializeField] private TileBase wateredTilledTile;

    private List<FarmTileInformation> tiles = new();

    private CropDatabase CropDatabase;

    private void Start()
    {
        CropDatabase = GetComponent<CropDatabase>();
    }

    public bool PlantSeed(string id, Vector3Int position)
    {
        FarmTileInformation ti;
        if (!GetTile(position, out ti)) return false;
        
        if (dirt.HasTile(position) != tilledTile) return false;

        if (ti.cropId != null) return false;
        
        string cropId = CropDatabase.ToCropId(id);
        if (cropId == null) return false;

        ti.cropId = cropId;
        UpdateTileMap(position);
        return true;
    }

    public void WaterSoil(Vector3Int position)
    {
        FarmTileInformation ti;
        if (GetTile(position, out ti))
        {
            if (ti.tilled) ti.watered = true;
            UpdateTileMap(position);
        }
    }

    public void TillSoil(Vector3Int position)
    {
        FarmTileInformation ti;
        if (!GetTile(position, out ti))
        {
            if (!dirt.HasTile(position)) return;
            if (dirt.GetTile(position) != untilledTile) return;
            
            ti = new FarmTileInformation(position);
            ti.tilled = true;
            tiles.Add(ti);

            UpdateTileMap(position);
        }
    }
    

    public void UpdateTileMap()
    {
        List<FarmTileInformation> toDelete = new();
        
        foreach (var ti in tiles)
        {
            if (ti.watered)
            {
                ti.watered = false;

                if (!String.IsNullOrEmpty(ti.cropId))
                {
                    ti.growthStage++;
                    
                    CropDatabaseAsset.CropData cropData = CropDatabase[ti.cropId];
                    if (ti.growthStage >= cropData.growthTime)
                    {
                        ti.finished = true;
                        
                        GameObject ob = Instantiate(cropData.monsterPrefab);
                        ob.transform.position = TileClicker.TileToWorldPos(ti.position);
                    }
                }
            }
            
            UpdateTileMap(ti.position);
            if(ti.finished) toDelete.Add(ti);
        }

        foreach (var VARIABLE in toDelete)
        {
            tiles.Remove(VARIABLE);
        }
    }
    

    public void UpdateTileMap(Vector3Int position)
    {
        FarmTileInformation ti;
        GetTile(position, out ti);
        
        //Resetting
        if (ti.finished)
        {
            crops.SetTile(position, null);
            dirt.SetTile(position, untilledTile);
            return;
        }
        
        //Dirt Condition
        if (ti.tilled)
        {
            if (ti.watered)
            {
                dirt.SetTile(position, wateredTilledTile);
            }
            else
            {
                dirt.SetTile(position, tilledTile);
            }
        }
        
        //Crop
        if (String.IsNullOrEmpty(ti.cropId)) return;
        
        CropDatabaseAsset.CropData cropData = CropDatabase[ti.cropId];
        TileBase tile = cropData.tiles[0].tile;
        for (int i = 1; i < cropData.tiles.Count; i++)
        {
            if (cropData.tiles[i].growthStage <= ti.growthStage)
            {
                tile = cropData.tiles[i].tile;
            }
            else
            {
                break;
            }
        }
        crops.SetTile(position, tile);
    }

    public bool GetTile(Vector3Int position, out FarmTileInformation t)
    {
        t = tiles.Find(tile => tile.position == position);
        return t != null;
    }
}
