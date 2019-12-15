using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuController : MonoBehaviour
{
    public GameObject buttonCanvas;
    public GameObject levelSelectionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenLevelSelectionWindow()
    {
        buttonCanvas.SetActive(false);
        levelSelectionCanvas.SetActive(true);
    }

    public void CloseLevelSelectionWindow()
    {
        buttonCanvas.SetActive(true);
        levelSelectionCanvas.SetActive(false);
    }
}
