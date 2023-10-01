using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GotoGame : MonoBehaviour
{
    public Material[] skyboxs;
    int i = 0;
    private void Start()
    {
        InvokeRepeating("Change", 10f, 10f);   
    }
    public void Load()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    
    void Change()
    {
        if (i >= skyboxs.Length)
        {
            i =0;
        }

        RenderSettings.skybox = skyboxs[i];
        i++;
    }
}
