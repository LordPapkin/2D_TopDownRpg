using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moveable : Killable
{
    protected BoxCollider2D boxCollider;
    protected Rigidbody2D rb;
    protected Vector2 moveDelta;          
    
    [Header("Speed Settings")]
    public float enemySpeed = 2f;
    [SerializeField] private bool facingRight = true;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Move(Vector3 input)
    {
        input.Normalize();
        moveDelta = input;       

        if (moveDelta.x > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveDelta.x < 0 && facingRight)
        {
            Flip();
        }
        //Add push force if any
        if (pushDirection != null)
            moveDelta += pushDirection;
        //Reduce push force every frame based od recovery speed
        pushDirection = Vector2.Lerp(pushDirection, Vector2.zero, pushRecoverySpeed);

        rb.MovePosition(rb.position + (moveDelta * enemySpeed * Time.fixedDeltaTime));
        
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
