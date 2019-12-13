using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Animator bodyAnimator;
    public SpriteRenderer bodySprite;
    public Rigidbody2D body;
    public float speed = 0;

    [SerializeField]
    private float movement = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bodyAnimator.SetBool("Falling", body.velocity.y < 0);
        body.position += Vector2.right * Time.deltaTime * speed * movement;
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

}
