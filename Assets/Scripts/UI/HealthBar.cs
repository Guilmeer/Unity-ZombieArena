using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    public float lastHealth;
    public float currentHealth;
    [SerializeField] GameObject player;
    private Player playerScript;
    [SerializeField] [Range(0, .2f)] float time;

    private void Start()
    {
        healthBar = GetComponent<Image>();
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {
        lastHealth = Mathf.Lerp(lastHealth, currentHealth, time);
        currentHealth = playerScript.Health;

        healthBar.fillAmount = lastHealth / playerScript.InitialHealth;

    }
}
