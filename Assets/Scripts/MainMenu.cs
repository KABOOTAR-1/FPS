using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;
using UnityEditor.UI;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    bool sound = true;
    [SerializeField]
    GameObject SoundIcon;

    public Sprite[] sprites;
    [SerializeField]
    AudioSource audio_source;
    // Update is called once per frame
   public void Resume()
    {
        
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Main()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void Sound()
    {
        sound = !sound;
        int x;
        if (sound == true)
        {
            x = 0;
            audio_source.Play();
        }
        else
        {
            audio_source.Stop();
            x = 1;
        }
            SoundIcon.transform.GetComponent<Image>().sprite = sprites[x];
       
        
    }

    public void Exit()
    {

      #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
      #endif
        Application.Quit();

    }
    
}
