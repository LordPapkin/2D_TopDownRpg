using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [Header("Torch Settings")]
    [SerializeField] int torchDamage = 1;
    [SerializeField] float cooldown = 1;
    private float lastburn;

    private void Start()
    {
        lastburn = -cooldown;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Enemy"))
        {
            if (Time.time - lastburn < cooldown)
                return;

            Damage dmg = new Damage(Vector3.zero, torchDamage, 0);
            collision.SendMessage("TakeDamage", dmg);
        }
    }
}
