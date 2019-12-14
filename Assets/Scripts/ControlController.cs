using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlController : MonoBehaviour
{
    public enum ControlType
    {
        RIGHT = 0,
        LEFT = 1,
        JUMP = 2
    }

    public AdventurerController adventurerController;

    [Header("Launching Attributes")]
    public GameObject controlPrefab;
    public float launchStartingDistance;
    public float initialLaunchForce = 0;

    [Header("Control Attributes")]
    public List<bool> availableControls;
    [SerializeField]
    private ControlType selectedControl;
    [SerializeField]
    private int selectedControlInt;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.instance.IsGamePaused())
        {
            return;
        }

        UpdateAvailableControls();
        UpdateSelectedControl();
    }

    private void UpdateAvailableControls()
    {
        rightControlRenderer.sprite = availableControls[(int)ControlType.RIGHT] ? rightAvailableSprite : rightNotAvailableSprite;
        leftControlRenderer.sprite = availableControls[(int)ControlType.LEFT] ? leftAvailableSprite : leftNotAvailableSprite;
        jumpControlRenderer.sprite = availableControls[(int)ControlType.JUMP] ? jumpAvailableSprite : jumpNotAvailableSprite;
    }

    private void UpdateSelectedControl()
    {

        selectedControl = IntToControlType(selectedControlInt);

        rightSelectedControlDisplay.SetActive(false);
        leftSelectedControlDisplay.SetActive(false);
        jumpSelectedControlDisplay.SetActive(false);

        if (!IsThereAvailableControl() || !IsSelectedControlAvailable() ||!adventurerController.IsSelectedAdventurer())
        {
            return;
        }

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
        if (direction == 0 || !IsThereAvailableControl())
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
        if (!availableControls[selectedControlInt])
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
        if (!availableControls[selectedControlInt])
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

        GameObject instantiatedControlGameObject = Instantiate(controlPrefab, initialLaunchPosition, Quaternion.identity);

        float forceMultiplier = Mathf.Lerp(0.1f, 1, power);
        instantiatedControlGameObject.GetComponent<Rigidbody2D>().AddForce(directionToMouse * initialLaunchForce * forceMultiplier);

        Control instantiatedControl = instantiatedControlGameObject.GetComponent<Control>();
        instantiatedControl.SetControlType(selectedControl);

        LoseControl(selectedControl);
    }

    public void GiveControl(ControlType control)
    {
        availableControls[(int)control] = true;
    }

    private void LoseControl(ControlType control)
    {
        availableControls[(int)control] = false;
    }

    public bool IsAvailable(ControlType controlType)
    {
        return availableControls[(int)controlType];
    }

    public bool IsThereAvailableControl()
    {
        return availableControls.Contains(true);
    }
    
    public bool IsSelectedControlAvailable()
    {
        return availableControls[(int)selectedControl];
    }

    private ControlType IntToControlType(int intControlType)
    {
        if (intControlType == 0)
        {
            return ControlType.RIGHT;
        }
        else if (intControlType == 1)
        {
            return ControlType.LEFT;
        }
        else if (intControlType == 2)
        {
            return ControlType.JUMP;
        }

        return ControlType.RIGHT;
    }
}
