using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Dmg Settings")]
    public int[] damage = {1,2,3,4,5,6,7,8};
    public float[] pushForce = { 1f, 1.5f, 1.8f, 2f, 2.2f, 2.4f, 2.6f, 2.8f };

    [Header("Weapon Upgrade Settings")]
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    [Header("Weapon Swing Settings")]
    private float cooldown = 0.3f;
    private float lastSwing;
    private Animator animator;


    private void Start()
    {        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("Enemy"))
        {
            Debug.Log(collision.collider.name);
            Damage dmg = new Damage(transform.position, damage[weaponLevel], pushForce[weaponLevel]);
            collision.collider.SendMessage("TakeDamage", dmg);
        }
    }

    private void Swing()
    {
        animator.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
       
    }
}
