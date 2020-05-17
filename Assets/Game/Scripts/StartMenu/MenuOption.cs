using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MenuOption : MonoBehaviour
{
    public AudioClip optionDescriptionSound;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void playSound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void playDescription()
    {
        audioSource.clip = optionDescriptionSound;
        audioSource.Play();

    }
    public void stopSound()
    {
        audioSource.Stop();
    }

    public abstract void enterOption();
}
