using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreSystem : MonoBehaviour
{
    int score;
    [SerializeField]
    TextMeshProUGUI Score;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void IncreaseScore()
    {
        score++;
        Score.text = "SCORE:"+score.ToString(); 
    }
}
