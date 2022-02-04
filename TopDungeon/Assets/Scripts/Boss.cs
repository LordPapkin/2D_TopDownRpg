using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [Header("Rotating Fireball Setting")]
    [SerializeField] private float[] fireballSpeed = { 2.5f, -2.5f};
    [SerializeField] private float distance = 2.5f;

    private Vector3 move = Vector3.zero;
    [SerializeField] private Transform[] fireballs;

    [Header("Firing Fireball Setting")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private float shotDistance = 10f;
    [SerializeField] private float cooldown = 10f;
    [SerializeField] private Transform spawnPoint;
    private float lastShot;

    protected override void Start()
    {
        base.Start();
        lastShot = -cooldown;
    }

    private void Update()
    {
        FireballMove();

        if (Vector3.Distance(playerTransform.position, transform.position) > shotDistance)
            return;
        if (Time.time - lastShot < cooldown)
            return;
        ShotFireball();
        lastShot = Time.time;
    }

    private void FireballMove()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            move.x = -Mathf.Cos(Time.time * fireballSpeed[i]) * distance;
            move.y = Mathf.Sin(Time.time * fireballSpeed[i]) * distance;
            fireballs[i].position = transform.position + move;
        }
    }
    private void ShotFireball()
    {
        Instantiate(fireball, spawnPoint.position, transform.rotation);
    }
}
