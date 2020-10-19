using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashHit : MonoBehaviour
{
    Color originalColor;
    public Color hitColor;
    float timeOfFlashing = -1f;

    SpriteRenderer spriteRenderer;

    public void FlashWhenHit(float timeWhenStarts)
    {
        timeOfFlashing = timeWhenStarts + 0.2f;
    }

    private void Start()
    {
        hitColor = new Color(131, 109, 90);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.material.color;
    }

    private void Update()
    {
        if (Time.time < timeOfFlashing)
        {
            spriteRenderer.material.color = hitColor;
        }
        else
        {
            ResetColor();
        }
    }

    void ResetColor()
    {
        spriteRenderer.material.color = originalColor;
    }
}
