using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject player, shadow;
    public Vector3 firstPosOfPlayer, secondPosOfPlayer;
    public GameObject explosion;
    public Sprite playerSprite, tRexSprite;

    [HideInInspector] public bool playerDied;
    [HideInInspector] public bool playerJumped;

    private Animator anim;
    private SpriteRenderer playerRenderer;
    private GameObject[] starEffect;

    private string jumpAnimation = "PlayerJump", changeLaneAnimation = "ChangeLane";
    private bool tRexTrigger;

    void Awake() 
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        anim = GetComponentInChildren<Animator>();

        playerRenderer = player.GetComponent<SpriteRenderer>();

        starEffect = GameObject.FindGameObjectsWithTag(MyTags.STAR_EFFECT);
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeLane();
        HandleJump();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.CompareTag(MyTags.OBSTACLE))
        {
            if (!tRexTrigger)
            {
                DieWithObstacle(target);
            }
            else
            {
                DestroyObstacle(target);
            }
        }
        else if (target.CompareTag(MyTags.T_REX))
        {
            tRexTrigger = true;
            playerRenderer.sprite = tRexSprite;
            target.gameObject.SetActive(false);

            // CALL SOUND MANAGER TO PLAY THE MUSIC

            StartCoroutine(TRexDuration());
        }
        else if (target.CompareTag(MyTags.STAR))
        {
            for (int i = 0; i < starEffect.Length; i++)
            {
                if (!starEffect[i].activeInHierarchy)
                {
                    starEffect[i].transform.position = target.transform.position;
                    starEffect[i].SetActive(true);
                    break;
                }
            }

            target.gameObject.SetActive(false);
            // CALL SOUND MANAGER TO PLAY THE MUSIC
            // GAMEPLAY CONTROLLER INCREASE STAR SCORE
        }
    }

    private void HandleChangeLane()
    {
        if (!playerJumped)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                anim.Play(changeLaneAnimation);
                transform.localPosition = secondPosOfPlayer;

                // PLAY THE SOUND
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                anim.Play(changeLaneAnimation);
                transform.localPosition = firstPosOfPlayer;

                // PLAY THE SOUND
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!playerJumped)
            {
                anim.Play(jumpAnimation);
                playerJumped = true;
            }
        }
    }

    private void Die()
    {
        playerDied = true;
        player.SetActive(false);
        shadow.SetActive(false);

        GameplayController.instance.moveSpeed = 0;
        //GameplayController.instance.GameOver();

        // CALL SOUND MANAGER TO PLAY PLAYER DEAD SOUND
        // CALL SOUND MANAGER TO PLAY GAME OVER
    }

    private void DieWithObstacle(Collider2D target)
    {
        Die();

        explosion.transform.position = target.transform.position;
        explosion.SetActive(true);
        target.gameObject.SetActive(false);

        // CALL SOUND MANAGER TO PLAY PLAYER DEAD SOUND
    }

    private IEnumerator TRexDuration()
    {
        yield return new WaitForSeconds(7f);

        if (tRexTrigger)
        {
            tRexTrigger = false;

            playerRenderer.sprite = playerSprite;
        }
    }

    private void DestroyObstacle(Collider2D target)
    {
        explosion.transform.position = target.transform.position;
        explosion.SetActive(false);     // turn off explosion if it's already on
        explosion.SetActive(true);

        target.gameObject.SetActive(false);

        // CALL SOUND MANAGER TO PLAY PLAYER DEAD SOUND
    }
}
