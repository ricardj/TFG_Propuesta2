using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1TutorialManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip initialClip;
    public float initialTime = 3f;

    public static Level1TutorialManager instance = null;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("playInitialAudio", initialTime);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void playInitialAudio()
    {
        audioSource.clip = initialClip;
        audioSource.Play();
    }
}
