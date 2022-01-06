using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    //Logic
    protected bool collected;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnCollect();
        }
    }
    protected virtual void OnCollect()
    {
        collected = true;
    }
}
 