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

    void Start()
    {
        string path = "Sprites/Player/hero" + GameManager.instance.selectedIndex + "_big";
        playerSprite = Resources.Load<Sprite>(path);
        playerRenderer.sprite = playerSprite;
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

            SoundManager.instance.PlayPowerUpSound();

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
            SoundManager.instance.PlayCoinSound();
            GameplayController.instance.UpdateStarScore();
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

                SoundManager.instance.PlayMoveLaneSound();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                anim.Play(changeLaneAnimation);
                transform.localPosition = firstPosOfPlayer;

                SoundManager.instance.PlayMoveLaneSound();
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
                SoundManager.instance.PlayJumpSound();
            }
        }
    }

    private void Die()
    {
        playerDied = true;
        player.SetActive(false);
        shadow.SetActive(false);

        GameplayController.instance.moveSpeed = 0;
        GameplayController.instance.GameOver();

        SoundManager.instance.PlayDeadSound();
        SoundManager.instance.PlayGameOverSound();
    }

    private void DieWithObstacle(Collider2D target)
    {
        Die();

        explosion.transform.position = target.transform.position;
        explosion.SetActive(true);
        target.gameObject.SetActive(false);


        SoundManager.instance.PlayDeadSound();
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

        SoundManager.instance.PlayDeadSound();

        GameplayController.instance.UpdateStarScore();
    }
}
