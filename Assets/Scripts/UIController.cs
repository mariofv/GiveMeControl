using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("Pause Menu")]
    public GameObject pauseMenuPanel;

    [Header("Controls")]
    public GameObject controlsPanel;

    [Header("Level Completed")]
    public GameObject levelCompletedPanel;
    public Text levelCompletedText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowCompletedLevelScreen(int level)
    {
        levelCompletedText.text = "LEVEL " + level + " COMPLETE!";
        levelCompletedPanel.SetActive(true);
    }

    public void ShowPauseMenu(bool active)
    {
        pauseMenuPanel.SetActive(active);
        if (!active)
        {
            controlsPanel.SetActive(false);
        }
    }

    public void ShowControlsMenu(bool active)
    {
        controlsPanel.SetActive(active);
    }
}
