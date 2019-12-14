using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    [Header("Body Attributes")]
    public Animator bodyAnimator;
    public SpriteRenderer bodySprite;
    public Rigidbody2D body;
    public float movementSpeed = 0;
    public float jumpSpeed = 0;
    [SerializeField]
    private float movement = 0;
    [SerializeField]
    private bool jumping = false;
    [SerializeField]
    private bool falling = false;

    [Header("Cursor")]
    public Texture2D defaultCursor;
    public Texture2D chargingCursor;

    [Header("Launching Attributes")]
    public GameObject controlPrefab;
    public float launchStartingDistance;
    public float initialLaunchForce = 0;

    [Header("Charge Bar Attributes")]
    public ChargeBar chargeBar;
    public float maxChargeTime = 0;
    [SerializeField]
    private float chargingTime = 0;
    [SerializeField]
    private float currentCharge = 0;
    [SerializeField]
    private bool charging = false;
    

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (charging)
        {
            chargingTime += Time.deltaTime;
            currentCharge = Mathf.Min(chargingTime / maxChargeTime, 1f);
            chargeBar.SetProgress(currentCharge);
        }


        jumping = body.velocity.y > 0;
        bodyAnimator.SetBool("Jumping", jumping);
        falling = body.velocity.y < -0.1;
        bodyAnimator.SetBool("Falling", falling);
        body.position += Vector2.right * Time.deltaTime * movementSpeed * movement;
    }

    public void SetMovement(float movement)
    {
        if (charging)
        {
            return;
        }

        bodyAnimator.SetBool("Walking", movement != 0);
        this.movement = movement;
        if (movement > 0)
        {
            bodySprite.flipX = false;
            chargeBar.transform.parent.localRotation = Quaternion.identity;
        }
        else if (movement < 0)
        {
            bodySprite.flipX = true;
            chargeBar.transform.parent.localRotation = Quaternion.AngleAxis(-180, Vector3.up);
        }
    }

    public void Jump()
    {
        if (charging)
        {
            return;
        }

        if (body.velocity.y == 0)
        {
            jumping = true;
            body.velocity += Vector2.up * jumpSpeed;
        }
    }

    public void StartCharging()
    {
        if (!jumping && !falling)
        {
            SetMovement(0);
            charging = true;
            Cursor.SetCursor(chargingCursor, Vector2.zero, CursorMode.Auto);
            chargeBar.transform.gameObject.SetActive(true);
            chargingTime = 0;
        }
    }

    public void EndCharging(Vector2 mousePosition)
    {
        if (charging)
        {
            charging = false;
            chargeBar.transform.gameObject.SetActive(false);
            LaunchControl(mousePosition);
        }
    }

    public void LaunchControl(Vector2 mousePosition)
    {
        Vector2 directionToMouse = (Camera.main.ScreenToWorldPoint(mousePosition) - transform.position).normalized;
        Cursor.SetCursor(defaultCursor, Vector2.zero, CursorMode.Auto);
        Vector3 initialLaunchOffset = directionToMouse * launchStartingDistance;
        Vector3 initialLaunchPosition = transform.position + initialLaunchOffset;

        GameObject instantiatedControl = Instantiate(controlPrefab, initialLaunchPosition, Quaternion.identity);

        float forceMultiplier = initialLaunchForce * Mathf.Lerp(0.75f, 1, currentCharge);
        instantiatedControl.GetComponent<Rigidbody2D>().AddForce(directionToMouse * initialLaunchForce * currentCharge);
    }
}
