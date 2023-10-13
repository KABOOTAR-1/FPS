using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    float health;

    private void Start()
    {
        health = 100f;
    }


    public void ChangeHealth()
    {
        health -= 5;
        if (health <= 0)
        {
            Player.Instance.IncreaseScore();
            Destroy(gameObject);
        }
    }
   
}
