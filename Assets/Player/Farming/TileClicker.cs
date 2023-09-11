using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileClicker : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction mouseAction;
    
    [SerializeField] private GameObject cursor;
    private SpriteRenderer cursorSp;

    private static float cellSize = 2;
    [SerializeField] private float range;

    public UnityEvent<Vector3Int> tileClicked;

    private Camera cam;
    
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
        
        Vector3 tilePos = WorldToTilePos(mousePos);

        cursor.transform.position = TileToWorldPos(tilePos);
        
        bool inRange = Vector3.Distance(mousePos, transform.position) < range * cellSize || Vector3.Distance(TileToWorldPos(tilePos), transform.position) < (range + 0.5) * cellSize;
        
        cursorSp.color = inRange ? new Color(1, 1, 1, 0.2f) : new Color(1, 0, 0, 0.2f);

        if (useAction.triggered && inRange)
        {
            tileClicked.Invoke(Vector3Int.FloorToInt(tilePos));
        }
    }

    public static Vector3Int WorldToTilePos(Vector3 position)
    {
        return new Vector3Int((int) Mathf.Floor(position.x / cellSize), (int) Mathf.Floor(position.y / cellSize), 0);
    }

    public static Vector3 TileToWorldPos(Vector3 position)
    {
        return position * cellSize + new Vector3(cellSize/2f, cellSize/2f, 0);
    }
}
