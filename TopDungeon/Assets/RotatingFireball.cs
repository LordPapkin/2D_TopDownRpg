using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingFireball : Killable
{
    [Header("Dmg Settings")]
    [SerializeField] private int damage = 1;
    [SerializeField] private float pushForce = 1;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Damage dmg = new Damage(transform.position, damage, pushForce);
            collision.collider.SendMessage("TakeDamage", dmg);
        }
    }
}
