﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmoothFade : MonoBehaviour
{
    // Inspector Fields
    public float Delay; // The delay before fading
    public float Duration; // The duration of the fade
    public bool FadeIn; // Check whether fading in or fading out

    private float currentTime;
    private float delayTime;
    private SpriteRenderer sr;
    private Image image;
    private Text text;

    private MenuButton menuButton;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
        text = GetComponent<Text>();
        menuButton = GetComponentInParent<MenuButton>();

        // Disable menu button for conflicting code
        if (menuButton != null)
            menuButton.enabled = false;

        // Set the initial times
        currentTime = 0;
        delayTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Delay before fade
        if (delayTime < Delay)
        {
            delayTime += Time.deltaTime;
        }
        // Sprite Renderer
        else if (sr != null)
        {
            // Lerp the fade in
            if (FadeIn && sr.color.a != 1)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Duration;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(0, 1, value));
            }
            // Lerp the fade out
            else if (!FadeIn && sr.color.a != 0)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Duration;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, Mathf.Lerp(1, 0, value));
            }
            // Enable button again
            else if (menuButton != null)
                menuButton.enabled = true;
        }
        // UI Image
        else if (image != null)
        {
            // Lerp the fade in
            if (FadeIn && image.color.a != 1)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Duration;
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(0, 1, value));
            }
            // Lerp the fade out
            else if (!FadeIn && image.color.a != 0)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Duration;
                image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(1, 0, value));
            }
            // Enable button again
            else if (menuButton != null)
                menuButton.enabled = true;
        }
        // UI Text
        else if (text != null)
        {
            // Lerp the fade in
            if (FadeIn && text.color.a != 1)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Duration;
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(0, 1, value));
            }
            // Lerp the fade out
            else if (!FadeIn && text.color.a != 0)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / Duration;
                text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, value));
            }
            // Enable button again
            else if (menuButton != null)
                menuButton.enabled = true;
        }
    }
}
