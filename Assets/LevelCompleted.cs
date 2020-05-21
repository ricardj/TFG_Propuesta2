using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleted : MonoBehaviour
{

    AudioSource audioSource;
    public AudioClip mainSoundtrack;
    public AudioClip optionSelectionSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = mainSoundtrack;
        audioSource.Play();

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            audioSource.PlayOneShot(optionSelectionSound);
            SceneManager.LoadScene("Menu");
        }
    }
}
