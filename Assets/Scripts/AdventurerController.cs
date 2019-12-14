using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerController : MonoBehaviour
{
    public GameObject selectedPlayerDisplay;
    public PlayerInputController playerInputController;
    public BodyController bodyController;

    [SerializeField]
    private bool selected;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActiveAdventurer(bool active)
    {
        selected = active;
        selectedPlayerDisplay.SetActive(active);
        playerInputController.enabled = active;

        if (!active)
        {
            bodyController.SetMovement(0);
        }
    }

    public bool IsSelectedAdventurer()
    {
        return selected;
    }
}
