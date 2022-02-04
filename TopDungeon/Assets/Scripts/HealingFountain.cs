using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : MonoBehaviour
{
    [Header("Healing Settings Settings")]
    [SerializeField] private int healingAmount = 1;
    [SerializeField] private int healingResources = 10;
    [SerializeField] private float cooldown = 1f;
    private float lastHeal;

    private void Start()
    {
        lastHeal = -cooldown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {        
        if (!collision.collider.CompareTag("Player"))
            return;
        if (Time.time - lastHeal < cooldown)
            return;
        if (GameManager.instance.player.currentHealth >= GameManager.instance.player.maxHealth)
            return;
        if (GameManager.instance.player.currentHealth + healingAmount >= GameManager.instance.player.maxHealth)        
            healingAmount = GameManager.instance.player.maxHealth - GameManager.instance.player.currentHealth;

        GameManager.instance.player.Heal(healingAmount);
        healingResources -= healingAmount;        
    }
}
