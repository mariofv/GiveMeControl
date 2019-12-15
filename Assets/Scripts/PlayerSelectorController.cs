using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorController : MonoBehaviour
{
    public List<AdventurerController> adventurerControllers;
    public AudioSource selectPlayerAudio;

    [SerializeField]
    private int currentSelectedPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.IsGamePaused())
        {
            return;
        }

        for (int i = 0; i < adventurerControllers.Count; ++i)
        {
            
            adventurerControllers[i].SetActiveAdventurer(i == currentSelectedPlayer);
        }
    }

    public void SetPreviousPlayer()
    {
        selectPlayerAudio.Play();
        if (currentSelectedPlayer - 1 == -1)
        {
            currentSelectedPlayer = adventurerControllers.Count - 1;
        }
        else
        {
            --currentSelectedPlayer;
        }
    }

    public void SetNextPlayer()
    {
        selectPlayerAudio.Play();
        if (currentSelectedPlayer + 1 == adventurerControllers.Count)
        {
            currentSelectedPlayer = 0;
        }
        else
        {
            ++currentSelectedPlayer;
        }
    }
}
