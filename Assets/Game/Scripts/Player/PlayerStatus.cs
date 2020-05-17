using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStatus : MonoBehaviour
{
    public float maxPlayerLife = 100f;
    float playerLife;
    float previousPlayerLife;
    public AudioClip playerDamagedSound;
    public AudioClip lifeBelowSeventySound;
    public AudioClip lifeBelowFiftySound;
    public AudioClip lifeBelowTenSound;

    AudioSource audioSource;

    //The player automatically gains life
    public float gainingLifePeriod = 2f;
    public float gainingLifeIncrement = 2f;
    float gainingLifeCounter = 0f;




    public enum HealthState { Full, High, Medium, Low };
    public HealthState currentHealthState;
    HealthState previousHealthState;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.LogError("Error: no audio source attached to player status");
        audioSource.loop = true;

        playerLife = maxPlayerLife;
        previousPlayerLife = playerLife;
        currentHealthState = HealthState.Full;
        previousHealthState = currentHealthState;

    }


    // Update is called once per frame
    void Update()
    {
        if (playerLife != previousPlayerLife)
        {
            if (playerLife < maxPlayerLife * 0.1f) currentHealthState = HealthState.Low;
            else if (playerLife < maxPlayerLife * 0.5f) currentHealthState = HealthState.Medium;
            else if (playerLife < maxPlayerLife * 0.7f) currentHealthState = HealthState.High;
            else if (playerLife >= maxPlayerLife * 0.7f) currentHealthState = HealthState.Full;
            previousPlayerLife = playerLife;
        }

        switch (currentHealthState)
        {
            case HealthState.Full:
                audioSource.Stop();
                break;
            case HealthState.High:
                if (audioSource.clip != lifeBelowSeventySound) audioSource.clip = lifeBelowSeventySound;
                break;
            case HealthState.Medium:
                if (audioSource.clip != lifeBelowFiftySound) audioSource.clip = lifeBelowFiftySound;
                break;
            case HealthState.Low:
                if (audioSource.clip != lifeBelowTenSound) audioSource.clip = lifeBelowTenSound;
                break;
        }

        if (currentHealthState != previousHealthState)
        {
            previousHealthState = currentHealthState;
            audioSource.Play();
        }

        //Automatic regeneration
        gainingLifeCounter += Time.deltaTime;
        if (playerLife < maxPlayerLife && gainingLifeCounter > gainingLifePeriod)
        {
            gainingLifeCounter = 0;
            playerLife += gainingLifeIncrement;
            playerLife = Mathf.Clamp(playerLife, 0, maxPlayerLife);
        }

        if (playerLife < 0)
        {
            executePlayerDeath();
        }
    }

    public void hitPlayer(float decreaseLife)
    {
        playerLife -= decreaseLife;
        audioSource.PlayOneShot(playerDamagedSound);
    }

    public void executePlayerDeath()
    {
        SceneManager.LoadScene("DeadMenu");

    }
}
