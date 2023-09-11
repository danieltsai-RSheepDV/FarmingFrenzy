using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toolbar_UI : MonoBehaviour
{
    [SerializeField] private List<GameObject> toolbarSlots = new List<GameObject>();

    private GameObject selectedSlot;

    private void Start(){
        SelectSlot(0);
    }

    private void Update(){
        CheckAlphaNumericKeys();
    }

    public void SelectSlot(int index){
        if(toolbarSlots.Count == 6){ // 6 or however many slots
            selectedSlot = toolbarSlots[index];
            Debug.Log("Selected Slot: " + selectedSlot.name);
        }
    }
    
    private void CheckAlphaNumericKeys(){
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(0);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha1)){
            SelectSlot(5);
        }

    }
}
