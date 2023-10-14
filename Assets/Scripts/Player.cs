using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player:MonoBehaviour
{
    int score=0;
    public static Player Instance;
    [SerializeField]
    TextMeshProUGUI Score;
    int health = 100;
    private void Start()
    {
        if (Instance != this)
        {
            Instance = this;
        }
        score = 0;
    }

    public void IncreaseScore()
    {
        score++;
        Score.text = "SCORE:" + score.ToString();
    }

    public void DecreaseHealth()
    {
        health -= 10;
        if (health <= 0)
        {
            Debug.Log("Player is dead");
            PutRequest();
        }
        else
        {
            Debug.Log(health);
        }
    }
    public void PutRequest()
    {
        string putUrl = ApiManager.MakeApiCall("update");
        StartCoroutine(ClientAPI.Put(putUrl, InputAndButtons.I_Username, score));
    }
}
