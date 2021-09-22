using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{

    [SerializeField]
    AudioClip enemyHitSound,
        enemyDyingSound, zombieSounds;
    float soundDelay = 0;
    float delay;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ResetAudioDelay();
    }

    // Update is called once per frame
    void Update()
    {

        if (!GetComponent<Enemy>().Dead)
        {
            if (soundDelay < delay)
            {
                soundDelay += Time.deltaTime;
            }
            else
            {
                PlayEnemyAudio("ZombieSound");
                ResetAudioDelay();
            }
        }
    }

    public void PlayEnemyAudio(string audioName)
    {
        switch (audioName)
        {
            case "Damage":
                audioSource.PlayOneShot(enemyHitSound, .08f);
                break;
            case "Died":
                audioSource.PlayOneShot(enemyDyingSound);
                break;
            case "ZombieSound":
                audioSource.PlayOneShot(zombieSounds);
                break;
        }
    }

    void ResetAudioDelay()
    {
        soundDelay = 0;
        float newDelay = new System.Random().Next(3, 15);
        delay = newDelay;
    }
}
