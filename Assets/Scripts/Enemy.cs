using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float health;
    public float iHealth { get; private set; }
    void Start()
    {
        
    }

    private void Update()
    {
        iHealth = health;
        if (health<=0)
            Destroy(gameObject);
    }

    public void ChangeHealth()
    {
        health -= 5;
    }
   
}
