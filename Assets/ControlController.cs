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

    [SerializeField]
    private Dictionary<ControlType, bool> availableControls;

    // Start is called before the first frame update
    void Start()
    {
        availableControls = new Dictionary<ControlType, bool>();
        availableControls.Add(ControlType.RIGHT, true);
        availableControls.Add(ControlType.LEFT, true);
        availableControls.Add(ControlType.JUMP, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsAvailable(ControlType controlType)
    {
        return availableControls[controlType];
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
