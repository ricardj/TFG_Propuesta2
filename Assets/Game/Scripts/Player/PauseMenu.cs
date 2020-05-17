using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    //Key Stuff
    public KeyCode pauseKey = KeyCode.P;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode enterKey = KeyCode.Return;
    public KeyCode repeatKey = KeyCode.R;

    //Sound stuff
    public AudioClip pauseMenuMusic;
    public AudioClip continueSound;
    public AudioClip exitSound;
    public AudioClip enterOptionSound;
    public AudioClip changeOptionSound;
    public AudioClip activationSound;

    AudioSource audioSource;

    //Index stuff

    int maxOptions = 2;
    int currentIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    bool pauseMenuActivated = false;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pauseKey) && pauseMenuActivated)
        {
            pauseMenuActivated = false;
            unPause();
        }
        else
        {
            if (Input.GetKeyDown(pauseKey) && !pauseMenuActivated)
            {
                //Debug.Log("Pause menú activated");
                pauseMenuActivated = true;
                pause();
            }

        }

        if (pauseMenuActivated)
        {
            if (Input.GetKeyDown(upKey))
            {
                moveUp();
                showOption();
            }
            if (Input.GetKeyDown(downKey))
            {
                moveDown();
                showOption();
            }
            if (Input.GetKeyDown(enterKey)) enterOption();

            if (Input.GetKeyDown(repeatKey)) showOption();
        }
    }

    public void enterOption()
    {
        audioSource.PlayOneShot(enterOptionSound);
        switch (currentIndex)
        {
            case 0:
                continueGame();
                break;
            case 1:
                exitGame();
                break;
        }
    }

    public void continueGame()
    {
        pauseMenuActivated = false;
        unPause();
    }

    public void exitGame()
    {
        SceneManager.LoadScene("Menu");
    }

    public void showOption()
    {
        switch (currentIndex)
        {
            case 0:
                audioSource.PlayOneShot(continueSound);
                break;
            case 1:
                audioSource.PlayOneShot(exitSound);
                break;
        }
    }

    public void moveUp()
    {
        if (currentIndex > 0) currentIndex--;
        audioSource.PlayOneShot(changeOptionSound);
    }

    public void moveDown()
    {
        if (currentIndex < maxOptions - 1) currentIndex++;
        audioSource.PlayOneShot(changeOptionSound);
    }


    public void pause()
    {
        Time.timeScale = 0;
        audioSource.clip = pauseMenuMusic;
        audioSource.Play();
    }

    public void unPause()
    {
        Time.timeScale = 1;
        audioSource.Stop();
    }
}
