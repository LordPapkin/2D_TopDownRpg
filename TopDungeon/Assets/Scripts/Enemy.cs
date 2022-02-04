using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Moveable
{    
    [Header("Loot Settings")]
    public int xpValue = 1;


    [Header("Logic Settings")]
    [SerializeField] private float triggerLenght = 1;
    [SerializeField] private float chaseLenght = 5;
    [SerializeField] private bool chasing;
    [SerializeField] private bool collidingWithPlayer;

    protected Transform playerTransform;
    protected Vector3 startingPosition;
    [SerializeField] protected float reachedPosition = 0.2f;
    

    //Hitbox
    private BoxCollider2D hitbox;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;

        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Is the player in range 
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLenght)
        {
            if(Vector3.Distance(playerTransform.position, startingPosition) < triggerLenght)
            {
                chasing = true;
            }

            if (chasing) 
            {
                if (!collidingWithPlayer)
                {  
                    Move((playerTransform.position - transform.position));
                }
            } 
            else
            {
                if (Vector3.Distance(transform.position, startingPosition) > reachedPosition)
                {
                    Move((startingPosition - transform.position));
                }                                     
            }
        }
        else
        {
            if (Vector3.Distance(startingPosition, transform.position) > reachedPosition)
            {
                Move((startingPosition - transform.position));
                chasing = false;
            }
        }        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collidingWithPlayer = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collidingWithPlayer = false;
        }
    }

    protected override void Die()
    {
        base.Die();
        GameManager.instance.GrantXP(xpValue);
        GameManager.instance.ShowText("+ " + xpValue + " exp", 30, Color.magenta, transform.position, Vector3.up * 40, 1f);
    }
}
