using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control : MonoBehaviour
{

    public ControlController.ControlType controlType;

    [Header("Control Sprites")]
    public Sprite rightSprite;
    public Sprite leftSprite;
    public Sprite jumpSprite;

    private SpriteRenderer controlSpriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        controlSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetControlType(ControlController.ControlType type)
    {
        controlType = type;
        switch (type)
        {
            case ControlController.ControlType.RIGHT:
                controlSpriteRenderer.sprite = rightSprite;
                break;
            case ControlController.ControlType.LEFT:
                controlSpriteRenderer.sprite = leftSprite;
                break;
            case ControlController.ControlType.JUMP:
                controlSpriteRenderer.sprite = jumpSprite;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ControlController>().GiveControl(controlType);
            Destroy(gameObject);
        }
    }
}
