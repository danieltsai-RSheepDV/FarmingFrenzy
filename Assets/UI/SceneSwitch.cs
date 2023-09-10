using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] string gameName;
    [SerializeField] string settingsName = "Settings";
    GameObject settingsPanel;
    // Start is called before the first frame update
    void Start()
    {
        settingsPanel = GameObject.FindGameObjectWithTag(settingsName);
        CloseSettings();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(gameName);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
}
