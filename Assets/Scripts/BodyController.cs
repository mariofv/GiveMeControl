﻿using System.Collections;
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
        Cursor.SetCursor(defaultCursor, new Vector2(defaultCursor.width / 2, defaultCursor.height / 2), CursorMode.Auto);
    }

    private void Update()
    {
        if (charging)
        {
            Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 chargeBarPosition = mousePosition2D + Vector2.down * 0.5f;
            Vector2 tramsformPosition2D = transform.position;

            chargeBar.transform.position = new Vector3(chargeBarPosition.x, chargeBarPosition.y, -1);
            chargingTime += Time.deltaTime;
            currentCharge = Mathf.Min(chargingTime / maxChargeTime, 1f);
            Vector2 directionToMouse = (mousePosition2D - tramsformPosition2D).normalized;
            if (directionToMouse.x > 0)
            {
                bodySprite.flipX = false;
            }
            else if (directionToMouse.x < 0)
            {
                bodySprite.flipX = true;
            }
            chargeBar.SetProgress(currentCharge);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
        }
        else if (movement < 0)
        {
            bodySprite.flipX = true;
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
            bodyAnimator.SetBool("Charging", charging);
            Cursor.SetCursor(chargingCursor, new Vector2(chargingCursor.width / 2, chargingCursor.height / 2), CursorMode.Auto);
            chargeBar.transform.gameObject.SetActive(true);
            chargingTime = 0;
        }
    }

    public void EndCharging()
    {
        if (charging)
        {
            charging = false;
            bodyAnimator.SetBool("Charging", charging);
            chargeBar.transform.gameObject.SetActive(false);
            LaunchControl();
        }
    }

    public void LaunchControl()
    {
        Vector2 mousePosition2D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 transformPosition2D = transform.position;

        Vector2 directionToMouse = (mousePosition2D - transformPosition2D).normalized;
        Cursor.SetCursor(defaultCursor, new Vector2(defaultCursor.width / 2, defaultCursor.height / 2), CursorMode.Auto);
        Vector3 initialLaunchOffset = directionToMouse * launchStartingDistance;
        Vector3 initialLaunchPosition = transform.position + initialLaunchOffset;

        GameObject instantiatedControl = Instantiate(controlPrefab, initialLaunchPosition, Quaternion.identity);

        float forceMultiplier = Mathf.Lerp(0.1f, 1, currentCharge);
        instantiatedControl.GetComponent<Rigidbody2D>().AddForce(directionToMouse * initialLaunchForce * forceMultiplier);
    }
}
