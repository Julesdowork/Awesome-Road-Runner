using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private Animator anim;

    private string walkAnimation = "PlayerWalk";

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void PlayerWalkAnimation()
    {
        anim.Play(walkAnimation);

        if (PlayerController.instance.playerJumped)
        {
            PlayerController.instance.playerJumped = false;
        }
    }

    private void AnimationEnded()
    {
        gameObject.SetActive(false);
    }
}
