using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float moveSpeed, distanceFactor = 1f;

    private float distanceMoved;
    private bool gameJustStarted;

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
            // CHECK IF PLAYER IS ALIVE
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

        // CHECK IF PLAYER IS ALIVE
        Camera.main.transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
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
}
