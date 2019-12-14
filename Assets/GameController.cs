using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Adventurers"), LayerMask.NameToLayer("Adventurers"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
