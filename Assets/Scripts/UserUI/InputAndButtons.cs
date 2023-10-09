using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.SceneManagement;
public class InputAndButtons : MonoBehaviour
{
    public GameObject[] login;
    public GameObject[] Register;
    public static string I_Username { get; private set; }
    void Start()
    {
        login[0].SetActive(true); ;
        Register[0].SetActive(true); ;
        login[1].SetActive(false);
        Register[1].SetActive(false);
    }

    // Update is called once per frame
    public void Login()
    {
        login[0].SetActive(false);
        Register[0].SetActive(false);
        login[1].SetActive(true);
    }

    public void register()
    {
        login[0].SetActive(false);
        Register[0].SetActive(false);
        Register[1].SetActive(true);
    }

    public void LoginInput()
    {
        int x = login[1].transform.childCount;
        GameObject[] loginchild = new GameObject[x - 1];
        for (int i = 0; i < x - 1; i++)
        {
            loginchild[i] = login[1].transform.GetChild(i).gameObject;
        }

        string url = ApiManager.MakeApiCall("login");
        string username = loginchild[0].transform.GetComponent<TMP_InputField>().text;
        string password = loginchild[1].transform.GetComponent<TMP_InputField>().text;
        I_Username = username;
        StartCoroutine(ClientAPI.LoginUser(url, username, password));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RegisterInput()
    {
        int x = Register[1].transform.childCount;
        GameObject[] registerchild = new GameObject[x - 1];
        for (int i = 0; i < x - 1; i++)
        {
            registerchild[i] = Register[1].transform.GetChild(i).gameObject;
        }

        string url = ApiManager.MakeApiCall("register");
        string username = registerchild[0].transform.GetComponent<TMP_InputField>().text;
        string password = registerchild[1].transform.GetComponent<TMP_InputField>().text;
        I_Username = username;
        StartCoroutine(ClientAPI.AddUser(url, username, password));
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}
