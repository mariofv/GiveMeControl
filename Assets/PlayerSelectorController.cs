using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorController : MonoBehaviour
{
    [Header("Player attributes")]
    public List<GameObject> selectedPlayerDisplays;
    public List<PlayerInputController> playerInputControllers;

    [SerializeField]
    private int currentSelectedPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < selectedPlayerDisplays.Count; ++i)
        {
            
            selectedPlayerDisplays[i].SetActive(i == currentSelectedPlayer);
            playerInputControllers[i].enabled = i == currentSelectedPlayer;
        }
    }

    public void SetPreviousPlayer()
    {
        if (currentSelectedPlayer - 1 == -1)
        {
            currentSelectedPlayer = selectedPlayerDisplays.Count - 1;
        }
        else
        {
            --currentSelectedPlayer;
        }
    }

    public void SetNextPlayer()
    {
        if (currentSelectedPlayer + 1 == selectedPlayerDisplays.Count)
        {
            currentSelectedPlayer = 0;
        }
        else
        {
            ++currentSelectedPlayer;
        }
    }
}
