using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GunShooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    ParticleSystem m_ParticleSystem;
    public WeaponManager m_WeaponManager;
    float recoil=0;
    public float range = 100f;
    public Animator anime;
    public Recoil recoil_script;
    public bool canFire = true;
    [SerializeField]
    float currentammo;
    [SerializeField]
    float magzine;
    TextMeshProUGUI ammo;
    RaycastHit hit;
    GameObject bulletMark;
    AudioSource m_AudioSource;
    Vector3 startPoint;
    Vector3 endPoint;
    float startTime;
    public void SetTextMeshPro(TextMeshProUGUI main)
    {
        ammo = main;
    }

    public void SetBulletMark(GameObject Mark)
    {
        bulletMark = Mark;
    }

    public void SetAudioSource(AudioSource audio)
    {
        m_AudioSource = audio;
    }

    public IEnumerator DoneGameManager(LayerMask Enemy,Camera cam,Action<Transform>onResult)
    {
        Transform val = null;
        if (currentammo > 0 || magzine > 0)
        {

            if (Input.GetKeyUp(KeyCode.R) || currentammo <= 0)
            {
                if (canFire)
                    StartCoroutine(Reload());
            }

            if (magzine <= 0)
                if (currentammo <= 0)
                {
                    val = null;
                }
            float start = Time.time;
            if (Input.GetMouseButtonDown(0) && canFire && recoil <= Time.time)
            {
                startTime = Time.time;
                if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range, Enemy))
                {
                    startPoint = cam.transform.position;
                    endPoint = hit.point;
                    if (bulletMark != null)
                    {
                        GameObject Mark = Instantiate(bulletMark, hit.point, Quaternion.LookRotation(hit.normal));
                        Destroy(Mark, 1);
                    }
                    val = hit.transform;
                }
                recoil = Time.time + 1 / m_WeaponManager.firerate;
                m_ParticleSystem.Play();
                currentammo--;
                if (m_AudioSource != null)
                    m_AudioSource.Play();
                recoil_script.recoil();
                anime.Play("Shoot");
                ammo.text = currentammo + "/" + magzine;
            }
            else
            {

                //anime.Play("idle");
            }
            float end = Time.time;
            onResult?.Invoke(val);
        }
  
       
        yield return null;
 
    }

    IEnumerator Reload()
    {
        if (currentammo == m_WeaponManager.ammo || magzine<=0)
            yield return null;

        canFire= false;
        yield return new WaitForSeconds(m_WeaponManager.TimeToReload);
        Debug.Log(m_WeaponManager.ammo -currentammo);
        magzine -= (m_WeaponManager.ammo - currentammo);
        currentammo = m_WeaponManager.ammo;
        canFire = true;
        ammo.text = currentammo + "/" + magzine;

    }

    void Start()
    {
        currentammo = m_WeaponManager.ammo;
        magzine = m_WeaponManager.magzine;
        ammo.text = currentammo + "/" + magzine;
    }
  
}
