using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public AudioClip rotationSound;
    public Transform playerBody;
    float xRotation = 0f;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        audioSource = GetComponent<AudioSource>();
    }

    public float mouseSensitivity = 100f;
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (mouseX > 0)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = rotationSound;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying) audioSource.Stop();
        }

        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);


    }
}
