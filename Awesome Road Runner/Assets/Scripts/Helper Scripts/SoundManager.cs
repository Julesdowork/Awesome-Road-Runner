using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource
        moveAudioSource,
        jumpAudioSource,
        powerUpAndDieAudioSource,
        backgroundAudioSource;
    public AudioClip
        powerUpClip,
        dieClip,
        coinClip,
        gameOverClip;

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
        
    }

    public void PlayMoveLaneSound()
    {
        moveAudioSource.Play();
    }

    public void PlayJumpSound()
    {
        jumpAudioSource.Play();
    }

    public void PlayDeadSound()
    {
        powerUpAndDieAudioSource.clip = dieClip;
        powerUpAndDieAudioSource.Play();
    }

    public void PlayPowerUpSound()
    {
        powerUpAndDieAudioSource.clip = powerUpClip;
        powerUpAndDieAudioSource.Play();
    }

    public void PlayCoinSound()
    {
        powerUpAndDieAudioSource.clip = coinClip;
        powerUpAndDieAudioSource.Play();
    }

    public void PlayGameOverSound()
    {
        backgroundAudioSource.Stop();
        backgroundAudioSource.clip = gameOverClip;
        backgroundAudioSource.loop = false;
        backgroundAudioSource.Play();
    }
}
