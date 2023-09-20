using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationScreen : MonoBehaviour
{
    private Canvas canvas;
    [SerializeField] private SaveVersion sv;
    
    void Start()
    {
        canvas = GetComponent<Canvas>();
        
        ToggleConfirmation(false);
    }

    public void ToggleConfirmation(bool b)
    {
        canvas.enabled = b;
    }

    public void DeleteSave()
    {
        SaveManager.Delete(sv.saveVersion);
    }
}
