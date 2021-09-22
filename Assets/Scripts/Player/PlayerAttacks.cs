using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{

    [SerializeField] GameObject bulletSpawnPoint;
    [SerializeField] float shootDelay;


    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    // Manages shooting using 
    private void Shoot()
    {
        // Aim
        if (Input.GetMouseButton(1))
        {
            PlayerAnim.instance.Aiming = true;
            // Shoot
            if (Input.GetMouseButtonDown(0))
            {
                // get bullet from bullet pool
                GameObject bullet = BulletPool.instance.GetBulletFromPool();

                // check if the bullet is null, if not, prepares it to be shot
                if (bullet != null)
                {
                    bullet.transform.position = bulletSpawnPoint.transform.position;
                    Vector3 mouseDirection = transform.GetComponent<PlayerMovements>().GetMouseDirection();
                    Quaternion rotation = transform.GetComponent<PlayerMovements>().GetPlayerRotation();
                    bullet.GetComponent<BasicBullet>().SetShot(GetComponent<Player>(), mouseDirection, rotation);
                    bullet.SetActive(true);

                    // Shot Audio
                    GetComponent<PlayerAudioManager>().PlayPlayerAudio("Shot");
                }
            }
        }
        if (Input.GetMouseButtonUp(1)) { PlayerAnim.instance.Aiming = false; }
    }
}
