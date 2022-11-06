using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    [SerializeField] Color lightColor = Color.green;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TurnOn()
    {
        spriteRenderer.color = lightColor;
    }
}
