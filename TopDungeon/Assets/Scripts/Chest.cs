using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    [SerializeField] private Sprite emptyChest;
    [SerializeField] private int gold = 100;
    protected override void OnCollect()
    {
        if (!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.gold += gold;
            GameManager.instance.ShowText("+" + gold + " gold coins!", 25, Color.yellow, transform.position, Vector3.up * 50, 1.5f);
            //Debug.Log("You got " + gold + " gold coins");
        }
    }
}
