using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    public BodyController bodyController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bodyController.SetMovement(Input.GetAxisRaw("Horizontal"));
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
            bodyController.EndCharging(Input.mousePosition);
        }

    }
}
