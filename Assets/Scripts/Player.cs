using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Player:MonoBehaviour
{
    int score;
    public static Player Instance;
    [SerializeField]
    TextMeshProUGUI Score;
    private void Start()
    {
        if (Instance != this)
        {
            Instance = this;
        }
    }

    public void IncreaseScore()
    {
        score++;
        Score.text = "SCORE:" + score.ToString();
    }
}
