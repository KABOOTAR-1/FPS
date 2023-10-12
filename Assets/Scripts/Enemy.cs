using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Enemy : MonoBehaviour
{
    [SerializeField]
    float health;
    ScoreSystem scoresystem;

    private void Start()
    {
        health = 100f;
        scoresystem=GameObject.FindGameObjectWithTag("GameManager").GetComponent<ScoreSystem>();
    }


    public void ChangeHealth()
    {
        health -= 5;
        if (health <= 0)
        {
            scoresystem.IncreaseScore();
            Destroy(gameObject);
        }
    }
   
}
