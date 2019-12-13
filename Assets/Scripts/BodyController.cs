using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Animator bodyAnimator;
    public SpriteRenderer bodySprite;
    public Rigidbody2D body;
    public float movementSpeed = 0;
    public float jumpSpeed = 0;

    [SerializeField]
    private float movement = 0;
    [SerializeField]
    private bool jumping = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        jumping = body.velocity.y > 0;
        bodyAnimator.SetBool("Jumping", jumping);
        bodyAnimator.SetBool("Falling", body.velocity.y < -0.1);
        body.position += Vector2.right * Time.deltaTime * movementSpeed * movement;
    }

    public void SetMovement(float movement)
    {
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
        if (body.velocity.y == 0)
        {
            jumping = true;
            body.velocity += Vector2.up * jumpSpeed;
        }
    }
}
