using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{

    public BodyController bodyController;
    public ControlController controlController;

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

        bodyController.SetMovement(Input.GetAxisRaw("Horizontal"));
        controlController.SetSelectedControl(Input.GetAxis("Mouse ScrollWheel"));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bodyController.Jump();
        }
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            bodyController.StartCharging();
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            bodyController.EndCharging();
        }

    }
}
