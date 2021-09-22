using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    [SerializeField] AudioClip playerHitSound, hurtSound, shotSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPlayerAudio(string audioName)
    {
        switch (audioName)
        {
            case "Damage":
                audioSource.PlayOneShot(playerHitSound, .5f);
                audioSource.PlayOneShot(hurtSound);
                break;
            case "Shot":
                audioSource.PlayOneShot(shotSound, .2f);
                break;
        }
    }
}
