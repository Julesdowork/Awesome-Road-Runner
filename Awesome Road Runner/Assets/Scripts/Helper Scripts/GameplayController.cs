using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float moveSpeed, distanceFactor = 1f;
    [HideInInspector] public bool obstaclesAreActive;

    public GameObject obstaclesObj;
    public GameObject[] obstacleList;
    public GameObject pausePanel;
    public Animator pauseAnim;
    public GameObject gameOverPanel;
    public Animator gameOverAnim;
    public Text finalScoreText, hiScoreText, finalStarScoreText;

    private float distanceMoved;
    private bool gameJustStarted;
    private string coroutineName = "SpawnObstacles";
    private int starScoreCount, scoreCount;

    [SerializeField] private Text scoreText;
    [SerializeField] private Text starScoreText;

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

        scoreCount = (int)round;    // save the score when the player dies
        scoreText.text = scoreCount.ToString();

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

    public void UpdateStarScore()
    {
        starScoreCount++;
        starScoreText.text = starScoreCount.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseAnim.Play("SlideIn");
    }

    public void ResumeGame()
    {
        pauseAnim.Play("SlideOut");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Gameplay");
    }

    public void HomeButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main Menu");
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        gameOverAnim.Play("SlideIn");

        finalScoreText.text = scoreCount.ToString();
        finalStarScoreText.text = starScoreCount.ToString();

        if (GameManager.instance.scoreCount < scoreCount)
            GameManager.instance.scoreCount = scoreCount;

        hiScoreText.text = GameManager.instance.scoreCount.ToString();

        GameManager.instance.starScore += starScoreCount;

        GameManager.instance.SaveGameData();
    }
}
