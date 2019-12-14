using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
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
}
