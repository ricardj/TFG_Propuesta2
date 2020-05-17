using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOGOption : MonoBehaviour
{

    public AudioClip nameSound;
    public AudioClip idleSound;
    public AudioClip enterSound;
    protected AudioSource audioSource;

    public string playerBodyTag = "Player";

    [HideInInspector]
    public Vector3 position;
    // Start is called before the first frame update
    public void Start()
    {
        position = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerBodyTag)
        {
            audioSource.PlayOneShot(enterSound);
        }
    }
}
