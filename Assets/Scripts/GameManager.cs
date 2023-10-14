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
    GameObject _Player;
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
        gunShooting.SetAudioSource(m_AudioSource);
        gunShooting.SetBulletMark(bulletMark);
        _Player = playerHealth.gameObject;
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
                StartCoroutine(gunShooting.DoneGameManager(Enemy, Camera.main, (val) =>
                {
                    if (val != null)
                    {
                        val.GetComponent<Enemy>().ChangeHealth();
                        StartCoroutine(ToDo(val));
                    }
                }));
            }
        }

    }

    IEnumerator ToDo(Transform val)
    {
        if (val != null)
        {
            float rotationSpeed = 3f;
            Quaternion startRotation = val.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(_Player.transform.position - val.position);

            float elapsedTime = 0f;

            while (val!=null && elapsedTime < 1f)
            {
                val.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
                elapsedTime += Time.deltaTime * rotationSpeed;
                yield return null;
            }
            if(val!=null)
            val.rotation = targetRotation;
        }

    }
}
