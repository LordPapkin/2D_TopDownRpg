using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;
    [Header("Ustawienia Gracza")]
    [SerializeField] private float speed;
    private Vector2 moveDelta;
    private bool facingRight = true;

    private float x;
    private float y;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");        
    }
    private void FixedUpdate()
    {
        //reset moveDelta
        moveDelta.x = x;
        moveDelta.y = y;

        if(moveDelta.x > 0 && !facingRight)
        {
            Flip();
        }else if (moveDelta.x < 0 && facingRight)
        {
            Flip();
        }

        rb.MovePosition(rb.position + moveDelta * speed * Time.fixedDeltaTime);
    }
    private void Flip()
    {
        //prosta zamiana wartoœci boola na przeciwn¹
        facingRight = !facingRight;
        // zmiana skali z 1 na -1 lub z -1 na 1 :)
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
