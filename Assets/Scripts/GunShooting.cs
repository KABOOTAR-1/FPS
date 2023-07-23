using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunShooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioSource m_AudioSource;
    [SerializeField]
    ParticleSystem m_ParticleSystem;
    public WeaponManager m_WeaponManager;
    float recoil=0;
    public float range = 100f;
    public GameObject Richrocket;
    GameObject flash;
    public Camera cam;
    RaycastHit hit;
    public Animator anime;
    public Recoil recoil_script;
    public RaycastHit iRay { get; private set; }
    public bool canFire = true;
    [SerializeField]
    float currentammo;
    [SerializeField]
    float magzine;
    [SerializeField]
    TextMeshProUGUI ammo;

    public void DoneGameManager()
    {
        Done();
    }
    void Done()
    {
        if (Input.GetKeyUp(KeyCode.R) || currentammo <= 0)
        {
            if (canFire)
                StartCoroutine(Reload());
        }

        if (magzine <= 0)
            if (currentammo <= 0)
                return;

        if (Input.GetMouseButtonDown(0) && canFire && recoil <= Time.time)
        {
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                if (hit.transform.GetComponent<Enemy>() != null)
                {
                    hit.transform.GetComponent<Enemy>().ChangeHealth();
                    flash = Instantiate(Richrocket, hit.point, Quaternion.LookRotation(hit.normal));
                    //flash.GetComponent<ParticleSystem>().Play();
                    Destroy(flash, 4);
                }

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
    }


    IEnumerator Reload()
    {
        if (currentammo == m_WeaponManager.ammo || magzine<=0)
            yield return null;

        canFire= false;
        yield return new WaitForSeconds(m_WeaponManager.TimeToReload);
        Debug.Log(m_WeaponManager.ammo -currentammo);
        magzine -= (7 - currentammo);
        currentammo = 7;
        canFire = true;
        
    }

    void Start()
    {
        currentammo = 7;
        magzine = m_WeaponManager.magzine;
    }
    // Update is called once per frame
  
}
