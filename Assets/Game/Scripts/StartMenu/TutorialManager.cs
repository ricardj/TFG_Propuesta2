using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float initialTime;
    public AudioClip[] tutorialClips;
    AudioSource audioSource;
    float initialitzationTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxTutorials = tutorialClips.Length;
    }

    public bool tutorialActivated = true;
    public int currentTutorial = -1;
    public int maxTutorials;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)
        || Input.GetKeyDown(KeyCode.W)
        || Input.GetKeyDown(KeyCode.A)
        || Input.GetKeyDown(KeyCode.S)
        || Input.GetKeyDown(KeyCode.D))
        {
            tutorialActivated = false;
        }

        if (tutorialActivated)
        {
            initialitzationTime += Time.deltaTime;
            if (initialitzationTime > initialTime)
            {
                if (audioSource.isPlaying)
                {
                    //Do nothing
                }
                else
                {
                    currentTutorial++;
                    if (currentTutorial < maxTutorials)
                    {
                        audioSource.clip = tutorialClips[currentTutorial];
                        audioSource.Play();
                    }
                    else
                    {
                        tutorialActivated = false;
                    }


                }
            }
        }
        else
        {
            audioSource.Stop();
        }
    }
}
