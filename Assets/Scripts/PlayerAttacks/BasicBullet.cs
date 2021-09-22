using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    private Player player;
    [SerializeField] float speed = 20;
    [SerializeField] Rigidbody rb;
    private Vector3 shotDirection;
    private Quaternion shotRotation;

    // set shot's player, direction and rotation
    public void SetShot(Player player, Vector3 shotDir, Quaternion rotation)
    {
        this.player = player;
        this.shotDirection = new Vector3(shotDir.normalized.x, 0, shotDir.normalized.z).normalized;
        transform.rotation = Quaternion.Euler(90, rotation.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = shotDirection * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        // if hits anything
        if (other.gameObject.tag != "Player")
        {
            if (other.gameObject.tag == "Inimigo" && other.GetComponent<Enemy>().IsAlive())
            {
                gameObject.SetActive(false);
                other.GetComponent<Enemy>().TakeDamage(player.Damage);
            }
            if (other.gameObject.tag != "Inimigo")
            {
                gameObject.SetActive(false);
            }
        }
    }
}
