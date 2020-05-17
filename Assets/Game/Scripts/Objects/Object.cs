using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{

    public AudioClip descriptionSound;
    public AudioClip idleSound;

    public float minPickUpDistance = 2f;

    AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    void Update()
    {

    }

    public void pickUp()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(descriptionSound);
    }
}
