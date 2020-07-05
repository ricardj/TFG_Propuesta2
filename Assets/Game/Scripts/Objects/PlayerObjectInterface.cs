using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectInterface : MonoBehaviour
{
    public bool allCodesPicked = false;
    public List<Object> objects = new List<Object>();
    public KeyCode pickUpKey = KeyCode.Q;


    public AudioClip pickUpSound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pickUpKey))
        {
            Object newObject = ObjectManager.instance.pickUpNearestAvaliableObject();
            if (newObject != null)
            {
                objects.Add(newObject);
                audioSource.PlayOneShot(pickUpSound);

                //TODO: improve that to check if its a code?
                if (objects.Count == 3) allCodesPicked = true;
            }
        }
    }
}
