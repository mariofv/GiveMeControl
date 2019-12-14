using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlController : MonoBehaviour
{
    public enum ControlType
    {
        RIGHT,
        LEFT,
        JUMP
    }

    [Header("Launching Attributes")]
    public GameObject controlPrefab;
    public float launchStartingDistance;
    public float initialLaunchForce = 0;

    [Header("Control Attributes")]
    public bool rightAvailable = true;
    public bool leftAvailable = true;
    public bool jumpAvailable = true;

    [Header("Display Control Attributes")]

    public SpriteRenderer rightControlRenderer;
    public Sprite rightAvailableSprite;
    public Sprite rightNotAvailableSprite;

    [Space(10)] // 10 pixels of spacing here.

    public SpriteRenderer leftControlRenderer;
    public Sprite leftAvailableSprite;
    public Sprite leftNotAvailableSprite;

    [Space(10)] // 10 pixels of spacing here.

    public SpriteRenderer jumpControlRenderer;
    public Sprite jumpAvailableSprite;
    public Sprite jumpNotAvailableSprite;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        rightControlRenderer.sprite = rightAvailable ? rightAvailableSprite : rightNotAvailableSprite;
        leftControlRenderer.sprite = leftAvailable ? leftAvailableSprite : leftNotAvailableSprite;
        jumpControlRenderer.sprite = jumpAvailable ? jumpAvailableSprite : jumpNotAvailableSprite;
    }

    public bool IsAvailable(ControlType controlType)
    {
        switch (controlType)
        {
            case ControlType.RIGHT:
                return rightAvailable;
            case ControlType.LEFT:
                return leftAvailable;
            case ControlType.JUMP:
                return jumpAvailable;
        }

        return false;
    }

    public void LaunchControl(float power)
    {
        Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 transformPosition2D = transform.position;

        Vector2 directionToMouse = (mousePosition2D - transformPosition2D).normalized;
        Vector3 initialLaunchOffset = directionToMouse * launchStartingDistance;
        Vector3 initialLaunchPosition = transform.position + initialLaunchOffset;

        GameObject instantiatedControl = Instantiate(controlPrefab, initialLaunchPosition, Quaternion.identity);

        float forceMultiplier = Mathf.Lerp(0.1f, 1, power);
        instantiatedControl.GetComponent<Rigidbody2D>().AddForce(directionToMouse * initialLaunchForce * forceMultiplier);
    }
}
