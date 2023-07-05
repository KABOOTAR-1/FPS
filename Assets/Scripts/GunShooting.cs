using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShooting : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioSource m_AudioSource;
    [SerializeField]
    ParticleSystem m_ParticleSystem;
    float recoil=0;
    public float firerate= 0.15f;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && recoil<=Time.time)
        {
            recoil = Time.time + 1 / firerate;
            m_ParticleSystem.Play();
            m_AudioSource.Play();
        }
    }
}
