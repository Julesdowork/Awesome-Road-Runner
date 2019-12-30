using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject player, shadow;
    public Vector3 firstPosOfPlayer, secondPosOfPlayer;

    [HideInInspector] public bool playerDied;
    [HideInInspector] public bool playerJumped;

    private Animator anim;

    private string jumpAnimation = "PlayerJump", changeLaneAnimation = "ChangeLane";

    void Awake() 
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

        anim = GetComponentInChildren<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleChangeLane();
        HandleJump();
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
}
