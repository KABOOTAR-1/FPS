using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.PostProcessing;
public class Player:MonoBehaviour
{
    int score=0;
    public static Player Instance;
    public PostProcessVolume postProcessVolune;
    [SerializeField]
    TextMeshProUGUI Score;
    Vignette vigentte;
    int health = 100;
    float maxIntensity = 0.39f;
    private void Start()
    {
        if (Instance != this)
        {
            Instance = this;
        }
        score = 0;
        postProcessVolune.profile.TryGetSettings(out vigentte);
        vigentte.intensity.value = 0;
    }

    public void IncreaseScore()
    {
        score++;
        Score.text = "SCORE:" + score.ToString();
    }

    public void DecreaseHealth()
    {
        health -= 10;
        StartCoroutine(HitDetect());
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

    IEnumerator HitDetect()
    {
        while(vigentte.intensity.value<maxIntensity)
        vigentte.intensity.value = Mathf.MoveTowards(vigentte.intensity.value, maxIntensity, Time.deltaTime*0.5f);
        yield return new WaitForSeconds(5f);
        while(vigentte.intensity.value>0)
        vigentte.intensity.value = Mathf.MoveTowards(vigentte.intensity.value, 0, Time.deltaTime *0.5f);

        yield return null;
    }
}
