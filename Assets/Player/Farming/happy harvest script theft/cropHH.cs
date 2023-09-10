using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cropHH : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

/*
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class cropHH : MonoBehaviour
{

    // <summary>
    /// Class used to designated a crop on the map. Store all the stage of growth, time to grow etc.
    // </summary>
    [CreateAssetMenu(fileName = "Crop", menuName = "2D Farming/Crop")]
    public class Crop : ScriptableObject, IDatabaseEntry
    {
        public string Key => UniqueID;

        public string UniqueID = "";
        
        public TileBase[] GrowthStagesTiles;
        
        public float GrowthTime = 1.0f;
        public int StageBeforeHarvest = 1;
        public VisualEffect HarvestEffect;

        public bool isWatered = 0;

        public void Grow(){
            StageBeforeHarvest += isWatered;
        }

        public int GetGrowthStage(float growRatio)
        {
            return StageBeforeHarvest;
        }
    }
}
*/
