using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyController : MonoBehaviour
{
    public Animator body_animator;
    public SpriteRenderer body_sprite;
    public Transform body_transform;

    [SerializeField]
    private float speed = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        body_transform.position += Vector3.right * Time.deltaTime * speed;
    }

    public void SetMovement(float movement)
    {
        body_animator.SetBool("Walking", movement != 0);
        speed = 10 * movement;
        if (movement > 0)
        {
            body_sprite.flipX = false;
        }
        else if (movement < 0)
        {
            body_sprite.flipX = true;
        }
    }

}
