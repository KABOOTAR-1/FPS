using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    Transform val;
    public void SetTextMeshPro(TextMeshProUGUI main)
    {
        ammo = main;
    }
    public Transform DoneGameManager(LayerMask Enemy,Camera cam,GameObject bulletMark, AudioSource m_AudioSource)
    {
        val =null;
        if(currentammo>0 || magzine>0)
        return Done(Enemy,cam,bulletMark,m_AudioSource);

        return val;
    }
    Transform Done(LayerMask Enemy,Camera cam, GameObject bulletMark, AudioSource m_AudioSource)
    {
        if (Input.GetKeyUp(KeyCode.R) || currentammo <= 0)
        {
            if (canFire)
                StartCoroutine(Reload());
        }

        if (magzine <= 0)
            if (currentammo <= 0)
                return null;

        if (Input.GetMouseButtonDown(0) &&canFire && recoil <= Time.time)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range,Enemy))
            {   
                    GameObject Mark = Instantiate(bulletMark, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(Mark, 1);
                    val = hit.transform;
            }
            recoil = Time.time + 1 / m_WeaponManager.firerate;
            m_ParticleSystem.Play();
            currentammo--;
            m_AudioSource.Play();
            recoil_script.recoil();
            anime.Play("Shoot");
        }
        else
        {

            //anime.Play("idle");
        }
        ammo.text = currentammo + "/" + magzine;
        return val;

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
