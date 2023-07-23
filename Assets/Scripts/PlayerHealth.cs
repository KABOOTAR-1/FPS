using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float health=100;
    public float I_Health { get; set; }
    void Start()
    {
     I_Health = health;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void DeductHealth(int damage)
    {
        health -= damage;
        I_Health = health;
    }
}
