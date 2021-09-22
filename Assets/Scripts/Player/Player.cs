using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : LivingBeing
{

    [SerializeField] GameObject retryMenu;

    private void Awake()
    {
        AudioListener.volume = 1;
    }
    public override void TakeDamage(float dmg)
    {
        if (!Dead) GetComponent<PlayerAudioManager>().PlayPlayerAudio("Damage");
        if (SceneManager.GetActiveScene().name == "Game")
        {
            base.TakeDamage(dmg);
        }
    }

    public override void Die()
    {
        StartCoroutine(DyingControl());
        AudioListener.volume = .1f;
    }
    IEnumerator DyingControl()
    {
        GetComponent<PlayerAnim>().Dead = true;
        Dead = true;
        yield return new WaitForSeconds(3);
        // Call Retry UI;
        if (SceneManager.GetActiveScene().name == "Game")
        {
            retryMenu.SetActive(true);
        }
        base.Die();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyHit"))
        {
            TakeDamage(other.gameObject.GetComponentInParent<Enemy>().Damage);
        }
    }
}
