using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingBeing : MonoBehaviour
{

    [SerializeField] float health, damage;
    [SerializeField] float initialHealth, initialDamage;

    private bool dead;

    public float Health { get => health; set => health = value; }
    public float Damage { get => damage; set => damage = value; }
    public bool Dead { get => dead; set => dead = value; }
    public float InitialHealth { get => initialHealth; set => initialHealth = value; }

    void Start()
    {
        Health = InitialHealth;
        Damage = initialDamage;
    }

    // Applies Damage taken to Health
    public virtual void TakeDamage(float dmg)
    {
        Health -= dmg;
        if (GetComponent<EventManager>()) GetComponent<EventManager>().CallDamage();
        if (Health <= 0) Die();
    }

    // Kill Living Being ( and zombie :P )
    public virtual void Die()
    {
        // Destroy(gameObject);
    }

    // Check if it is alive
    public bool IsAlive()
    {
        return Health > 0;
    }
}
