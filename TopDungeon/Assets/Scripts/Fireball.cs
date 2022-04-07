using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [Header("Fireball Settings")]
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    [SerializeField] private float pushForce;
    [SerializeField] private bool facingRight = false;
    [SerializeField] private float duration = 10f;

    
    [SerializeField] private Transform player;
    private Vector3 target;
    private Vector2 moveDelta;
    private Rigidbody2D rb;


    private void Start()
    {
        player = GameManager.instance.player.transform;
        target = player.position;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("UpdateTarget", 0f, 0.3f);
        Destroy(gameObject, duration);
    }    
    private void FixedUpdate()
    {
        Move(target - transform.position);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Damage dmg = new Damage(transform.position, damage, pushForce);
            collision.SendMessage("TakeDamage", dmg);
            Destroy(gameObject);
        }
    }
    private void UpdateTarget()
    {
        target = player.position;       
    }
    protected virtual void Move(Vector3 input)
    {
        input.Normalize();        
        moveDelta.x = input.x;
        moveDelta.y = input.y;

        if (moveDelta.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDelta.x < 0 && facingRight)
        {
            Flip();
        }        
        rb.MovePosition(rb.position +(moveDelta * speed * Time.fixedDeltaTime));
    }
    private void Flip()
    {
        //Every flip changes bool to match
        facingRight = !facingRight;
        //Simple scale change
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
