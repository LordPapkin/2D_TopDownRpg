using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ui : MonoBehaviour
{
    private void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("UI").Length > 1)
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {        
        DontDestroyOnLoad(this.gameObject);
    }
}
