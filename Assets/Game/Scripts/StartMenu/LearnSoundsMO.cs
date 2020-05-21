using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnSoundsMO : MenuOption
{
    public List<SoundDescription> soundDescriptions = new List<SoundDescription>();

    public override void enterOption()
    {
        Debug.Log("Desplegar menu descripción-sonido");
        MenuManager.instance.learnOptionMenu();

        //Get all components in child
        SoundDescription[] allChildComponents = GetComponentsInChildren<SoundDescription>();
        foreach (var item in allChildComponents)
        {
            soundDescriptions.Add(item);
        }


    }


    public void playOption(int index)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundDescriptions[index].descriptionSound);
    }
    public void enterLearnOption(int index)
    {
        if (index == 0)
        {
            MenuManager.instance.exitLearnOption();
        }
        audioSource.clip = soundDescriptions[index].sound;
        audioSource.Play();
        //audioSource.PlayOneShot(soundDescriptions[index].sound);
    }
}
