using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    public float cursorDelay;
    public List<GameObject> playerIcons;
    public List<GameObject> activeDelays;
    public float delay;

    [HideInInspector]
    public int players;

    private float cursorTimer;
    private float activeTimer;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject); // Allows the Manager object to continue to the next scene

        players = 1;
        cursorTimer = 0;
        activeTimer = 0;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeCursor();
        ActiveObjectDelay();
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

    void ActiveObjectDelay()
    {      
        if (activeTimer < delay)
            activeTimer += Time.deltaTime;
        else
        {
            foreach (GameObject gameObj in activeDelays)
                gameObj.SetActive(true);
        }
    }

    public void AddPlayer()
    {
        players++;
        if (players > 4) players = 4;

        for (int i = 0; i < players; i++)
            playerIcons[i].SetActive(true);
    }

    public void RemovePlayer()
    {
        players--;
        if (players < 1) players = 1;

        for (int i = playerIcons.Count - 1; i > players - 1; i--)
            playerIcons[i].SetActive(false);
    }

    public void StartGame()
    {
        // Don't start with 1 player
        if (players < 2) return;

        GameManager gm = GetComponent<GameManager>();
        gm.enabled = true;
        gm.players = players;
        gm.playersAlive = players;

        gm.BeginGame();
        enabled = false;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
