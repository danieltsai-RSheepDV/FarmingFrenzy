using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class watercanHH : MonoBehaviour
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

namespace HappyHarvest
{
    [CreateAssetMenu(fileName = "WaterCan", menuName = "2D Farming/Items/Water Can")]
    public class WaterCan : Item
    {
        public override bool CanUse(Vector3Int target)
        {
            return GameManager.Instance.Terrain.IsTilled(target);
        }

        public override bool Use(Vector3Int target)
        {
            GameManager.Instance.Terrain.WaterAt(target);
            return true;
        }
    }
}
*/