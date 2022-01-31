using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [Header("HP Settings")]
    public int currentHealth = 10;
    public int maxHealth = 10;
    [SerializeField] protected float immuneTime = 1.0f;
    public float pushRecoverySpeed = 0.2f;
    
    protected float lastImmune;

    protected Vector2 pushDirection;

    //ka¿dy kto dziedziczy t¹ klasê mo¿e otrzymaæ obra¿enia i umrzeæ
    protected virtual void TakeDamage(Damage dmg)
    {
        if(Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            currentHealth -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 25, Color.red, transform.position, Vector3.up * 20, 0.5f);
        } 

        if(currentHealth <= 0)
        {
            Die();
        }

    }
    protected virtual void Die()
    {
        Destroy(gameObject);
    }

}
