﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelectorController : MonoBehaviour
{
    public List<AdventurerController> adventurerControllers;

    [SerializeField]
    private int currentSelectedPlayer = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < adventurerControllers.Count; ++i)
        {
            
            adventurerControllers[i].SetActiveAdventurer(i == currentSelectedPlayer);
        }
    }

    public void SetPreviousPlayer()
    {
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
