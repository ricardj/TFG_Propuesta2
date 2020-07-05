using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelector : MonoBehaviour
{
    public GameObject mainGun;
    public GameObject secondaryGun;

    public KeyCode mainGunKey = KeyCode.Alpha1;
    public KeyCode secondaryGunKey = KeyCode.Alpha2;

    public AudioClip gunChangeSound;

    public AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        mainGun.SetActive(true);
        secondaryGun.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(mainGunKey))
        {
            mainGun.SetActive(true);
            secondaryGun.SetActive(false);
            audioSource.PlayOneShot(gunChangeSound);
        }
        if (Input.GetKeyDown(secondaryGunKey))
        {
            mainGun.SetActive(false);
            secondaryGun.SetActive(true);
            audioSource.PlayOneShot(gunChangeSound);
        }
    }
}
