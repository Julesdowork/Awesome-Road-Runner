using System.Collections;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float moveSpeed, distanceFactor = 1f;
    [HideInInspector] public bool obstaclesAreActive;

    public GameObject obstaclesObj;
    public GameObject[] obstacleList;

    private float distanceMoved;
    private bool gameJustStarted;
    private string coroutineName = "SpawnObstacles";

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        gameJustStarted = true;

        GetObstacles();
        StartCoroutine(coroutineName);
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        if (gameJustStarted)
        {
            if (!PlayerController.instance.playerDied)
            {
                if (moveSpeed < 12f)
                {
                    moveSpeed += Time.deltaTime * 5f;
                }
                else
                {
                    moveSpeed = 12f;
                    gameJustStarted = false;
                }
            }
        }

        if (!PlayerController.instance.playerDied)
        {
            Camera.main.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
            UpdateDistance();
        }
    }

    private void UpdateDistance()
    {
        distanceMoved += Time.deltaTime * distanceFactor;
        float round = Mathf.Round(distanceMoved);

        // COUNT AND SHOW THE SCORE

        if (round >= 30f && round < 60f)
        {
            moveSpeed = 14f;
        }
        else if (round >= 60f)
        {
            moveSpeed = 16f;
        }
    }

    private void GetObstacles()
    {
        obstacleList = new GameObject[obstaclesObj.transform.childCount];

        for (int i = 0; i < obstacleList.Length; i++)
        {
            obstacleList[i] =
                obstaclesObj.GetComponentsInChildren<ObstacleHolder>(true)[i].gameObject;
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (!PlayerController.instance.playerDied)
            {
                if (!obstaclesAreActive)
                {
                    if (Random.value <= 0.85f)
                    {
                        int randomIndex = 0;

                        do
                        {
                            randomIndex = Random.Range(0, obstacleList.Length);
                        } while (obstacleList[randomIndex].activeInHierarchy);

                        obstacleList[randomIndex].SetActive(true);
                        obstaclesAreActive = true;
                    }
                }
            }
            yield return new WaitForSeconds(0.6f);
        }
    }
}
