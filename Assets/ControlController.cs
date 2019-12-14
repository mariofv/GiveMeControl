using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlController : MonoBehaviour
{
    public enum ControlType
    {
        RIGHT,
        LEFT,
        JUMP,
        NONE
    }

    [Header("Launching Attributes")]
    public GameObject controlPrefab;
    public float launchStartingDistance;
    public float initialLaunchForce = 0;

    [Header("Control Attributes")]
    public bool rightAvailable = true;
    public bool leftAvailable = true;
    public bool jumpAvailable = true;

    [SerializeField]
    private ControlType selectedControl;
    [SerializeField]
    private int selectedControlInt = 0;
    [SerializeField]
    private List<bool> availableControl;


    [Header("Display Control Attributes")]
    public GameObject rightSelectedControlDisplay;
    public SpriteRenderer rightControlRenderer;
    public Sprite rightAvailableSprite;
    public Sprite rightNotAvailableSprite;

    [Space(10)] // 10 pixels of spacing here.

    public GameObject leftSelectedControlDisplay;
    public SpriteRenderer leftControlRenderer;
    public Sprite leftAvailableSprite;
    public Sprite leftNotAvailableSprite;

    [Space(10)] // 10 pixels of spacing here.

    public GameObject jumpSelectedControlDisplay;
    public SpriteRenderer jumpControlRenderer;
    public Sprite jumpAvailableSprite;
    public Sprite jumpNotAvailableSprite;


    // Start is called before the first frame update
    void Start()
    {
        availableControl.Add(rightAvailable);
        availableControl.Add(leftAvailable);
        availableControl.Add(jumpAvailable);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAvailableControls();
        UpdateSelectedControl();
    }

    private void UpdateAvailableControls()
    {
        rightControlRenderer.sprite = rightAvailable ? rightAvailableSprite : rightNotAvailableSprite;
        leftControlRenderer.sprite = leftAvailable ? leftAvailableSprite : leftNotAvailableSprite;
        jumpControlRenderer.sprite = jumpAvailable ? jumpAvailableSprite : jumpNotAvailableSprite;

        availableControl[0] = rightAvailable;
        availableControl[1] = leftAvailable;
        availableControl[2] = jumpAvailable;
    }

    private void UpdateSelectedControl()
    {
        if (selectedControlInt == 0)
        {
            selectedControl = ControlType.RIGHT;
        }
        else if (selectedControlInt == 1)
        {
            selectedControl = ControlType.LEFT;
        }
        else if (selectedControlInt == 2)
        {
            selectedControl = ControlType.JUMP;
        }

        rightSelectedControlDisplay.SetActive(false);
        leftSelectedControlDisplay.SetActive(false);
        jumpSelectedControlDisplay.SetActive(false);

        switch (selectedControl)
        {
            case ControlType.RIGHT:
                rightSelectedControlDisplay.SetActive(true);
                break;
            case ControlType.LEFT:
                leftSelectedControlDisplay.SetActive(true);
                break;
            case ControlType.JUMP:
                jumpSelectedControlDisplay.SetActive(true);
                break;
        }
    }

    public void SetSelectedControl(float direction)
    {
        if (direction == 0)
        {
            return;
        }

        if (direction > 0)
        {
            IncreaseSelectedControl();
        }
        else
        {
            DecreaseSelectedControl();
        }
    }

    private void IncreaseSelectedControl()
    {
        if (selectedControlInt + 1 == 3)
        {
            selectedControlInt = 0;
        }
        else
        {
            ++selectedControlInt;
        }
        if (!availableControl[selectedControlInt])
        {
            IncreaseSelectedControl();
        }
    }

    private void DecreaseSelectedControl()
    {
        if (selectedControlInt - 1 == -1)
        {
            selectedControlInt = 2;
        }
        else
        {
            --selectedControlInt;
        }
        if (!availableControl[selectedControlInt])
        {
            DecreaseSelectedControl();
        }
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

        SpriteRenderer instantiatedControlRenderer = instantiatedControl.GetComponent<SpriteRenderer>();
        switch (selectedControl)
        {
            case ControlType.RIGHT:
                instantiatedControlRenderer.sprite = rightAvailableSprite;
                break;
            case ControlType.LEFT:
                instantiatedControlRenderer.sprite = leftAvailableSprite;
                break;
            case ControlType.JUMP:
                instantiatedControlRenderer.sprite = jumpAvailableSprite;
                break;
        }

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
}
