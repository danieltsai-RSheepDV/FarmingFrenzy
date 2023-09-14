using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(CropDatabase))]
public class StructureManager : MonoBehaviour
{
    [SerializeField] private Tilemap structures;
    [SerializeField] private Tilemap dirt;

    private List<StructureTileInformation> tiles = new();

    private StructureDatabase StructureDatabase;

    private void Start()
    {
        StructureDatabase = GetComponent<StructureDatabase>();
    }

    public bool PlaceStructure(string id, Vector3Int position)
    {
        if (dirt.GetTile(position)) return false;
        
        StructureTileInformation ti;
        if (!GetTile(position, out ti))
        {
            ti = new StructureTileInformation(position);

            string structureId = StructureDatabase.ToItemId(id);
            if (structureId == null) return false;
            
            ti.structureId = structureId;
            
            tiles.Add(ti);
            
            UpdateTileMap(position);
            
            return true;
        }
        
        return false;
    }
    

    public void UpdateTileMap(Vector3Int position)
    {
        StructureTileInformation ti;
        GetTile(position, out ti);

        if (ti.destroyed)
        {
            structures.SetTile(position, null);
        }
        else
        {
            structures.SetTile(position, StructureDatabase[ti.structureId].tile);
        }
    }

    public bool GetTile(Vector3Int position, out StructureTileInformation t)
    {
        t = tiles.Find(tile => tile.position == position);
        return t != null;
    }
}
