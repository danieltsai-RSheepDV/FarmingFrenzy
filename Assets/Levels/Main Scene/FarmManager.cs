using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class FarmManager : MonoBehaviour
{
    private List<TileInformation> tiles;

    public void UpdateTile(Vector3Int position)
    {
        
    }

    private bool GetTile(Vector3Int position, out TileInformation t)
    {
        t = tiles.Find(tile => tile.position == position);
        return t == null;
    }
}
