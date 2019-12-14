using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public PlayerSelectorController playerSelectorController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerSelectorController.SetNextPlayer();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerSelectorController.SetPreviousPlayer();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameController.instance.TogglePauseGameMenu();
        }
    }
}
