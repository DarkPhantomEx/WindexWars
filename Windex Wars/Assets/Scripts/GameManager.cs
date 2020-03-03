using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public Rect spawnArea;
    public float spawnHeight;
    public float winCamDuration;
    public GameObject winSprite;

    [HideInInspector]
    public int players;
    [HideInInspector]
    public int playersAlive;
    [HideInInspector]
    public List<GameObject> playersObjAlive;

    private Camera winCam;
    private float currentTime;
    private Rect startRect;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(winSprite);
        DontDestroyOnLoad(winSprite.GetComponentInParent<GameObject>());

        players = 0;
        playersAlive = 0;

        playersObjAlive = new List<GameObject>();
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
        for (int i = 0; i < players; i++)
        {
            GameObject currPlayer = Instantiate(player);

            // Calculate random spawn position
            float xPos = Random.Range(spawnArea.min.x, spawnArea.max.x);
            float zPos = Random.Range(spawnArea.min.y, spawnArea.max.y);

            currPlayer.transform.position = new Vector3(xPos, spawnHeight, zPos);
            playersObjAlive.Add(currPlayer);
        }
    }
}
