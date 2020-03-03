using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    public Text text; // The text to modify
    public Color color; // The color to change
    public float Duration; // The duration of the color change

    private float currentTime;
    private bool hover;
    private Color initialColor;
    private Color currentColor;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        hover = false;

        initialColor = text.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (hover && text.color != color)
        {
            currentTime += Time.deltaTime;
            float value = currentTime / Duration;
            text.color = new Color(Mathf.Lerp(currentColor.r, color.r, value), Mathf.Lerp(currentColor.g, color.g, value), Mathf.Lerp(currentColor.b, color.b, value), currentColor.a);
        }
        else if (!hover && text.color != initialColor)
        {
            currentTime += Time.deltaTime;
            float value = currentTime / Duration;
            text.color = new Color(Mathf.Lerp(currentColor.r, initialColor.r, value), Mathf.Lerp(currentColor.g, initialColor.g, value), Mathf.Lerp(currentColor.b, initialColor.b, value), currentColor.a);
        }
    }

    public void OnMouseEnter()
    {
        currentColor = text.color;
        hover = true;
        currentTime = 0;
    }

    public void OnMouseExit()
    {
        currentColor = text.color;
        hover = false;
        currentTime = 0;
    }
}
