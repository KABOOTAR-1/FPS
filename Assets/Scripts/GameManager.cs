using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject Mainmenu;
    [SerializeField]
    GameObject MainCamera;
    [SerializeField]
    PlayerHealth playerHealth;
    public GunShooting gunShooting;
    [SerializeField]
    TextMeshProUGUI User_Name;
    public LayerMask Enemy;
    [SerializeField]
    GameObject bulletMark;
    [SerializeField]
    AudioSource m_AudioSource;
    [SerializeField]
    TextMeshProUGUI GunAmmo;
    private void Awake()
    {
        Time.timeScale = 0.2f;
    }
    void Start()
    {
        Mainmenu.SetActive(false);
        gunShooting = MainCamera.GetComponentInChildren<GunShooting>();
        User_Name.text = InputAndButtons.I_Username;
        gunShooting.SetTextMeshPro(GunAmmo);
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
            if (Input.GetMouseButtonDown(0) || Input.GetKeyUp(KeyCode.R))
            {
                Transform EnemyPlayer = gunShooting.DoneGameManager(Enemy, Camera.main, bulletMark,m_AudioSource);
                if (EnemyPlayer != null)
                EnemyPlayer.GetComponent<Enemy>().ChangeHealth();
            }
        }

    }
}
