using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(InventoryManager))]
public class FarmingActionController : MonoBehaviour
{
    private const string WATERING_CAN = "Item_WateringCan";
    private const string RAKE = "Item_Rake";
    
    // Tile Selection
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction mouseAction;
    
    [SerializeField] private GameObject cursor;
    private SpriteRenderer cursorSp;
    private Camera cam;
    private Animator anim;
    private FarmManager farmManager;
    private StructureManager structureManager;
    private ItemDatabase itemDatabase;
    private InventoryManager inventoryManager;

    [SerializeField] private float range;
    private float timeToUse;
    private static float cellSize = 2;
    
    private Vector3 tilePos;
    private float timer;
    private bool taskFinished;

    private void Start()
    {
        farmManager = GameManager.FarmManager;
        structureManager = GameManager.StructureManager;
        inventoryManager = GetComponent<InventoryManager>();
        itemDatabase = GameManager.ItemDatabase;
        cam = Camera.main;
        anim = GetComponent<Animator>();
        cursorSp = cursor.GetComponent<SpriteRenderer>();
        
        useAction = PI.FindAction("Use");
        mouseAction = PI.FindAction("Mouse");
    }
    
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        mousePos.z = 0;
        Vector3 newTilePos = WorldToTilePos(mousePos);
        if (tilePos != newTilePos)
        {
            timer = 0;
            taskFinished = false;
        }
        tilePos = newTilePos;

        cursor.transform.position = TileToWorldPos(tilePos);
        
        bool inRange = Vector3.Distance(mousePos, transform.position) < range * cellSize || Vector3.Distance(TileToWorldPos(tilePos), transform.position) < (range + 0.5) * cellSize;
        cursorSp.color = inRange ? new Color(1, 1, 1, 0.2f) : new Color(1, 0, 0, 0.2f);

        if (useAction.triggered)
        {
            timeToUse = itemDatabase[inventoryManager.GetSelectedItem().itemId].timeToUse;
        }
        
        if (useAction.inProgress && inRange)
        {
            if (timer < timeToUse)
            {
                anim.SetBool("Use", true);
                
                timer += Time.deltaTime;
                cursor.transform.localScale = Vector3.Lerp(cellSize * Vector3.one, Vector3.zero, timer/timeToUse);
            }
            else if (!taskFinished)
            {
                anim.SetBool("Use", false);
                
                UseItem(Vector3Int.FloorToInt(tilePos));
                taskFinished = true;
                
                cursor.transform.localScale = cellSize * Vector3.one;
            }
        }
        else
        {
            anim.SetBool("Use", false);
            
            taskFinished = false;
            timer = 0;
            
            cursor.transform.localScale = cellSize * Vector3.one;
        }
    }

    private static Vector3Int WorldToTilePos(Vector3 position)
    {
        return new Vector3Int(Mathf.FloorToInt(position.x / cellSize), Mathf.FloorToInt(position.y / cellSize), 0);
    }

    public static Vector3 TileToWorldPos(Vector3 position)
    {
        return position * cellSize + new Vector3(cellSize/2f, cellSize/2f, 0);
    }

    public void UseItem(Vector3Int position)
    {
        try
        {
            if (inventoryManager.GetSelectedItem() == null)
            {
                Debug.Log("Attempted to use nothing! Not an error, just kinda weird.");
                return;
            }
            
            ItemDatabaseAsset.ItemData itemData = itemDatabase[inventoryManager.GetSelectedItem().itemId];
            if (itemData.tags.Contains("seed"))
            {
                if(farmManager.PlantSeed(itemData.id, position)) inventoryManager.RemoveItem(itemData.id);
            }
            else if(itemData.tags.Contains("structure"))
            {
                if (structureManager.PlaceStructure(itemData.id, position)) inventoryManager.RemoveItem(itemData.id);
            }
            else if(itemData.tags.Contains("path"))
            {
                if (structureManager.PlacePath(itemData.id, position)) inventoryManager.RemoveItem(itemData.id);
            }
            else if (itemData.id == WATERING_CAN)
            {
                farmManager.WaterSoil(position);
            }
            else if (itemData.id == RAKE)
            {
                farmManager.TillSoil(position);
            }
        }
        catch (KeyNotFoundException e)
        {
            Debug.LogError(e);
        }
    }
}
