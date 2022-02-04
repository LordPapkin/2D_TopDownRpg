using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Killable
{
   
    private BoxCollider2D boxCollider;
    private Rigidbody2D rb;

    [Header("Ustawienia Gracza")]
    [SerializeField] private float speed;

    private Vector2 moveDelta;
    private bool facingRight = true;

    private float x;
    private float y;

    private SpriteRenderer spriteRenderer;
    public int spirte_number = 0;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = GameManager.instance.playerSprites[spirte_number];        
        HealthBar.instance.UpdateHealthBar();
        DontDestroyOnLoad(this);
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

        //Add push force if any
        if (pushDirection != null)
            moveDelta += pushDirection;
        //Reduce push force every frame based od recovery speed
        pushDirection = Vector2.Lerp(pushDirection, Vector2.zero, pushRecoverySpeed);

        rb.MovePosition(rb.position + moveDelta * speed * Time.fixedDeltaTime);
    }

    public void ChangeSprite(int currentCharacterSelection)
    {
        spirte_number = currentCharacterSelection;
        spriteRenderer.sprite = GameManager.instance.playerSprites[currentCharacterSelection];
    }

    public void LevelUp()
    {
        Debug.Log("level up");
        maxHealth += 5;
        currentHealth = maxHealth;        
        HealthBar.instance.UpdateHealthBar();
        GameManager.instance.ShowText("Level Up!", 30, Color.magenta, transform.position, Vector3.up * 30, 1.5f);

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

    protected override void TakeDamage(Damage dmg)
    {
        base.TakeDamage(dmg);
        HealthBar.instance.UpdateHealthBar();
    }
    public void Heal(int healingAmount)
    {
        currentHealth += healingAmount;
        HealthBar.instance.UpdateHealthBar();
        GameManager.instance.ShowText("+" + healingAmount, 30, Color.green, transform.position, Vector3.up * 30, 0.7f);
    }
}
