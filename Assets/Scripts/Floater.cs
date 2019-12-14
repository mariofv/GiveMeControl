using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [Header("Config")]
    public float floatSpeed = 0;
    public float floatAmplitude = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.parent.position + Vector3.up * (Mathf.Sin(Time.time * floatSpeed) * floatAmplitude + 0.75f);
    }
}
