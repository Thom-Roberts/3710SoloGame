using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip victorySound;
    public AudioClip mainSound;
    public AudioClip defeatSound;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0.15f;
        audioSource.clip = mainSound;
        audioSource.Play();
    }

    public void PlayMain() {
        audioSource.clip = mainSound;
        audioSource.Play();
    }

    public void PlayVictory() {
        audioSource.clip = victorySound;
        audioSource.Play();
    }

    public void DefeatSound() {
        audioSource.clip = defeatSound;
        audioSource.Play();
    }
}
