using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public static HealthBar instance;
    private float ratio;
    private Vector3 scale = Vector3.one;

    private void Awake()
    {
        if (HealthBar.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    public void UpdateHealthBar()
    {
        ratio = (float)GameManager.instance.player.currentHealth / (float)GameManager.instance.player.maxHealth;
        scale.x = ratio;
        transform.localScale = scale;
    }
}
