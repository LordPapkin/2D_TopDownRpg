using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTextPerson : MonoBehaviour
{
    [SerializeField] private string msg;

    private float cooldown = 4f;
    private float lastMsg;

    private void Start()
    {
        lastMsg = -cooldown;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") == false)
            return;
        if (Time.time - lastMsg < cooldown)
            return;

        lastMsg = Time.time;
        GameManager.instance.ShowText(msg, 20, Color.white, transform.position + Vector3.up, Vector3.zero, 4f);
    }
}
