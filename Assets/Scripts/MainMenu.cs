using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    bool sound = true;
    [SerializeField]
    GameObject SoundIcon;
    public Sprite[] sprites;
    [SerializeField]
    AudioSource audio_source;
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
        if (SceneManager.GetActiveScene().buildIndex +1 == SceneManager.sceneCountInBuildSettings)
        {
            Player.Instance.PutRequest();
        }

        #if UNITY_EDITOR
          EditorApplication.isPlaying = false;
        #endif
          Application.Quit();

    }
    
}
