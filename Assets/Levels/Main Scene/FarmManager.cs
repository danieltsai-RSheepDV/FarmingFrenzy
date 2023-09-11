using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FarmManager : MonoBehaviour
{
    [SerializeField] private Tilemap dirt;
    [SerializeField] private Tilemap crops;
    [SerializeField] private TileBase untilledTile;
    [SerializeField] private TileBase tilledTile;
    [SerializeField] private TileBase wateredTilledTile;
    [SerializeField] private TileBase stage1PotatoTile;
    [SerializeField] private TileBase stage2PotatoTile;
    [SerializeField] private TileBase stage1PeaTile;
    [SerializeField] private TileBase stage2PeaTile;
    [SerializeField] private GameObject pea;
    [SerializeField] private GameObject potato;

    private List<TileInformation> tiles = new();

    public void TillSoil(Vector3Int position)
    {
        TileInformation ti;
        if (!GetTile(position, out ti))
        {
            if (!dirt.HasTile(position)) return;
            if (dirt.GetTile(position) != untilledTile) return;
            
            ti = new TileInformation(position);
            ti.tilled = true;
            tiles.Add(ti);

            UpdateTileMap(position);
        }
    }
    

    public void UpdateTileMap()
    {
        List<TileInformation> toDelete = new();
        
        foreach (var ti in tiles)
        {
            if (ti.watered)
            {
                ti.watered = false;
                ti.growthStage++;

                if (ti.type == TileInformation.Types.POTATO && ti.growthStage == 2)
                {
                    ti.finished = true;
                    
                    GameObject ob = Instantiate(potato);
                    ob.transform.position = TileClicker.TileToWorldPos(ti.position);
                }else if (ti.type == TileInformation.Types.PEA && ti.growthStage == 3)
                {
                    ti.finished = true;
                    
                    GameObject ob = Instantiate(pea);
                    ob.transform.position = TileClicker.TileToWorldPos(ti.position);
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
        TileInformation ti;
        GetTile(position, out ti);

        if (ti.finished)
        {
            crops.SetTile(position, null);
            dirt.SetTile(position, untilledTile);
            return;
        }
        
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

        if (ti.type == TileInformation.Types.POTATO)
        {
            if (ti.growthStage == 0)
            {
                crops.SetTile(position, stage1PotatoTile);
            }else if (ti.growthStage == 1)
            {
                crops.SetTile(position, stage2PotatoTile);
            }
        }else if (ti.type == TileInformation.Types.PEA)
        {
            if (ti.growthStage == 0)
            {
                crops.SetTile(position, stage1PeaTile);
            }else if (ti.growthStage == 2)
            {
                crops.SetTile(position, stage2PeaTile);
            }
        }
    }

    public bool GetTile(Vector3Int position, out TileInformation t)
    {
        t = tiles.Find(tile => tile.position == position);
        return t != null;
    }
}
