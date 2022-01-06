using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Collidable
{
    private GameManager gameManager;

    protected override void Start()
    {
        base.Start();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            gameManager.NextLevel();
        }
    }
}
