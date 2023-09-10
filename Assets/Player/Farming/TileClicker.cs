using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileClicker : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction mouseAction;

    [SerializeField] private Tilemap tm;
    [SerializeField] private Tilemap tm2;
    [SerializeField] private GameObject cursor;
    private SpriteRenderer cursorSp;

    [SerializeField] private float cellSize = 2;
    [SerializeField] private float range;

    public UnityEvent<TileBase> tileClicked;

    private Camera cam;

    public Tile tilled;

    public Tile seededPotato;

    public Tile grownPotato;

    private Vector3 tilePos;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;

        cursorSp = cursor.GetComponent<SpriteRenderer>();
        
        useAction = PI.FindAction("Use");
        mouseAction = PI.FindAction("Mouse");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        mousePos.z = 0;
        
        tilePos = WorldToTilePos(mousePos);

        cursor.transform.position = TileToWorldPos(tilePos);
        
        bool inRange = Vector3.Distance(mousePos, transform.position) < range * cellSize || Vector3.Distance(TileToWorldPos(tilePos), transform.position) < (range + 0.5) * cellSize;
        
        cursorSp.color = inRange ? new Color(1, 1, 1, 0.2f) : new Color(1, 0, 0, 0.2f);

        if (useAction.triggered && inRange)
        {
            tileClicked.Invoke(tm.GetTile(Vector3Int.FloorToInt(tilePos)));
            //printTile(tm.GetTile(Vector3Int.FloorToInt(tilePos)));
        }
    }

    private Vector3Int WorldToTilePos(Vector3 position)
    {
        return new Vector3Int((int) Mathf.Floor(position.x / cellSize), (int) Mathf.Floor(position.y / cellSize), 0);
    }

    private Vector3 TileToWorldPos(Vector3 position)
    {
        return position * cellSize + new Vector3(cellSize/2f, cellSize/2f, 0);
    }

    public void printTile(TileBase t)
    {
        Debug.Log(t);
    }

    public void progressTile(TileBase tb){
        Debug.Log("hi");

        TileBase tb2 = tm2.GetTile(Vector3Int.FloorToInt(tilePos));
        
        Debug.Log(tb2.name);
        if(tb.name == "FarmTileset_8"){
            tm.SetTile(Vector3Int.FloorToInt(tilePos), tilled);
            Debug.Log("untilled");
            
        }else if(tm2.GetTile(Vector3Int.FloorToInt(tilePos)).name == "FarmTileset_4"){
            Debug.Log("baby potato");
            tm2.SetTile(Vector3Int.FloorToInt(tilePos), grownPotato);
        }else if(tb.name == "FarmTileset_7"){
            Debug.Log("tilled");
            tm2.SetTile(Vector3Int.FloorToInt(tilePos), seededPotato);
        }
    }
}
