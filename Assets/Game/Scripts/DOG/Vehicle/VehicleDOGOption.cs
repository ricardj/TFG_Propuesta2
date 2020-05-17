using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class VehicleDOGOption : DOGOption
{


    public AudioClip runningSound;
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;
    public AudioClip arrivalNotificationSound;


    NavMeshAgent navMeshAgent;

    Vector3 previousPosition;
    bool moving;
    public float minNotificationArrivalDistance;
    void Start()
    {
        base.Start();
        previousPosition = transform.position;
        moving = false;
        audioSource.loop = true;

        navMeshAgent = GetComponent<NavMeshAgent>();
        turnOff();
        finalDestination = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((finalDestination - transform.position).magnitude < minNotificationArrivalDistance
            && moving)
        {
            moving = false;
            audioSource.PlayOneShot(arrivalNotificationSound);
        }
        position = transform.position;

    }

    public void turnOn()
    {
        audioSource.PlayOneShot(turnOnSound);
        navMeshAgent.enabled = true;
    }
    public void turnOff()
    {

        navMeshAgent.enabled = false;
        audioSource.Stop();
        audioSource.PlayOneShot(turnOffSound);
    }

    public float radiusOffset = 6f;
    Vector3 finalDestination;
    public void goTo(OutdoorDOGOption otherBuilding)
    {
        //We dont go exactly to the position. We go to an offset.
        Vector3 offset = (transform.position - otherBuilding.enterPoint.position).normalized * radiusOffset;
        finalDestination = otherBuilding.enterPoint.position + offset;
        navMeshAgent.SetDestination(finalDestination);
        audioSource.clip = runningSound;
        audioSource.Play();
        moving = true;
    }

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("The vehicle has collided with " + collider.gameObject.name);
        //If the player enters
        if (collider.gameObject.tag == playerBodyTag)
        {
            audioSource.PlayOneShot(enterSound);
        }

    }
}
