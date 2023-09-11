using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolbarController : MonoBehaviour
{
    [SerializeField] int toolbarSize = 9;
    int selectedTool;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        float delta  = Input.mouseScrollDelta.y;
        if(delta != 0){
            if(delta > 0){
                selectedTool += 1;
                selectedTool = (selectedTool >= toolbarSize ? 0 : selectedTool);
            }else{
                selectedTool -= 1;
                selectedTool  = (selectedTool <= 0 ? toolbarSize - 1 : selectedTool);
            }
            Debug.Log(selectedTool);
        }
    }
}
