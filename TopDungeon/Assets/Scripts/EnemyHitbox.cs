using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    [Header("Dmg Settings")]
    public int damage;
    public float pushForce;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Damage dmg = new Damage(transform.position, damage, pushForce);
            collision.collider.SendMessage("TakeDamage", dmg);
        }
    }
}
