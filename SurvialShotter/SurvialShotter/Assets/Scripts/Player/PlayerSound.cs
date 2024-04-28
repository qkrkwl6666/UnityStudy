using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound Instance 
    { 
        get 
        { 
            if(!m_instance)
            {
                m_instance = GameObject.FindWithTag("Player").GetComponent<PlayerSound>();
            }
            return m_instance;
        } 
    }

    private static PlayerSound m_instance;

    private AudioSource audioSource;
    private AudioClip deathClip;
    private AudioClip gunShotClip;
    private AudioClip hurtClip;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        deathClip = (AudioClip)Resources.Load("Player Death");
        gunShotClip = (AudioClip)Resources.Load("Player GunShot");
        hurtClip = (AudioClip)Resources.Load("Player Hurt");
    }

    public void PlayerDeathSound()
    {
        audioSource.PlayOneShot(deathClip);
    }

    public void PlayerGunShotSound()
    {
        audioSource.PlayOneShot(gunShotClip);
    }

    public void PlayerHurtSound()
    {
        audioSource.PlayOneShot(hurtClip);
    }
}
