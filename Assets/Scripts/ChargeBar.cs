using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeBar : MonoBehaviour
{
    [Header("Fill Bar")]
    public Transform fillBar;
    public SpriteRenderer fillBarSprite;

    [Header("Config")]
    public Color initColor;
    public Color endColor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetProgress(float progress)
    {
        fillBar.localScale = new Vector3(Mathf.Lerp(0, 0.47f, progress), fillBar.localScale.y, fillBar.localScale.z);
        fillBarSprite.color = Color.Lerp(initColor, endColor, progress);
    }
}
