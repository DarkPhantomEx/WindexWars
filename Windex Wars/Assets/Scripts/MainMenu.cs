using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public float cursorDelay;

    private float cursorTimer;

    // Start is called before the first frame update
    void Start()
    {
        cursorTimer = 0;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCursor();
    }

    void ChangeCursor()
    {
        if (cursorTimer < cursorDelay)
            cursorTimer += Time.deltaTime;
        else
        {
            Cursor.visible = true;
            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
        }
    }
}
