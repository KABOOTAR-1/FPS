using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Mainmenu;
    bool pan = false;
    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    PlayerHealth playerHealth;
    public GunShooting gunShooting;
    [SerializeField]
    TextMeshProUGUI User_Name;

    [SerializeField]
    TextMeshProUGUI Score;


    private void Awake()
    {
        Time.timeScale = 0.2f;
    }
    void Start()
    {
        Mainmenu.SetActive(false);
        gunShooting = MainCamera.GetComponentInChildren<GunShooting>();
        User_Name.text = InputAndButtons.I_Username;

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Mainmenu.activeSelf == true)
            {
                Mainmenu.SetActive(false);
            }
            else
            {
                Mainmenu.SetActive(true);
            }
        }

        if (Mainmenu.activeSelf == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            gunShooting.DoneGameManager();
        }
        //Debug.Log(playerHealth.I_Health);

    }
}
