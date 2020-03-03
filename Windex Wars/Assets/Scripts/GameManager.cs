using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Rect spawnArea;
    public float spawnHeight;
    public float winCamDuration;
    public GameObject winSprite;
    public List<GameObject> deactivateUI;

    [HideInInspector]
    public int players;
    [HideInInspector]
    public int playersAlive;
    [HideInInspector]
    public List<GameObject> playersObjAlive;

    private GameObject playerPrefab;
    private Camera winCam;
    private float currentTime;
    private Rect startRect;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(GameObject.Find("Canvas"));
    }

    // Update is called once per frame
    void Update()
    {
        // Winner
        if (playersObjAlive.Count == 1)
        {
            if (winCam == null)
            {
                winCam = playersObjAlive[0].GetComponentInChildren<Camera>();
                currentTime = 0;
                startRect = winCam.rect;
            }
            else if (currentTime < winCamDuration)
            {
                currentTime += Time.deltaTime;
                float value = currentTime / winCamDuration;
                winCam.rect.Set(Mathf.Lerp(startRect.x, 0, value), Mathf.Lerp(startRect.y, 0, value), Mathf.Lerp(startRect.width, 1, value), Mathf.Lerp(startRect.height, 1, value));
            }
            else if (!winSprite.activeInHierarchy)
                winSprite.SetActive(true);
        }
    }

    public void BeginGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            // Instantiate
            playerPrefab = GameObject.Find("Player");

            playersObjAlive = new List<GameObject>();
            playersObjAlive.Add(playerPrefab);

            // Reverse everything
            foreach (GameObject gameObj in deactivateUI)
                gameObj.SetActive(false);

            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            // Create players
            for (int i = 0; i < players; i++)
            {
                GameObject currPlayer = playersObjAlive[0];

                if (i != 0)
                {
                    currPlayer = Instantiate(playerPrefab);

                    // Set player number
                    currPlayer.GetComponentInChildren<PlayerController>().PlayerNumber = i + 1;
                    currPlayer.GetComponentInChildren<CameraController>().PlayerNumber = i + 1;

                    // Calculate random spawn position
                    float xPos = Random.Range(spawnArea.min.x, spawnArea.max.x);
                    float zPos = Random.Range(spawnArea.min.y, spawnArea.max.y);

                    currPlayer.transform.position = new Vector3(xPos, spawnHeight, zPos);
                    playersObjAlive.Add(currPlayer);
                }

                // Set the camera
                switch (players)
                {
                    case 2:
                        if (i == 0)
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 1, 0.5f);
                        else
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                        break;
                    case 3:
                        if (i == 0)
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                        else if (i == 1)
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                        else
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                        break;
                    case 4:
                        if (i == 0)
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                        else if (i == 1)
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                        else if (i == 2)
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                        else
                            currPlayer.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                        break;
                }
            }
        }
    }
}
