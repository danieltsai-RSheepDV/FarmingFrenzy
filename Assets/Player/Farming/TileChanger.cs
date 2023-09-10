/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileChanger : MonoBehaviour
{
    [SerializeField] private Tilemap tm;
    [SerializeField] private GameObject cursor;

    public Tile tilled;

    public Tile seededPotato;
    
    
    public void progressTile(TileBase tb){

        if(tb.name == "FarmTileset_8"){
            
            tm.SetTile(Vector3Int.FloorToInt(tilePos), tilled);
            Debug.Log("untilled");
            
        }else if(tb.name == "FarmTileset_7"){
            Debug.Log("tilled");
            tm.SetTile(Vector3Int.FloorToInt(tilePos), seededPotato);
        }
    }
    
    private Vector3Int WorldToTilePos(Vector3 position)
    {
        return new Vector3Int((int) Mathf.Floor(position.x / cellSize), (int) Mathf.Floor(position.y / cellSize), 0);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
};*/
