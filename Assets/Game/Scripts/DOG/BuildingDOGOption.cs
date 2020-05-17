using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDOGOption : DOGOption
{
    // Start is called before the first frame update
    List<DoorDOGOption> roomDoors;
    void Start()
    {
        base.Start();
        roomDoors = new List<DoorDOGOption>();
        DoorDOGOption[] aux = gameObject.GetComponentsInChildren<DoorDOGOption>();
        foreach (DoorDOGOption element in aux)
        {
            roomDoors.Add(element);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public DOGOption getNearestDoor()
    {
        Vector3 playerPosition = NavigationManager.instance.playerDogInterface.gameObject.transform.position;
        DoorDOGOption nearestDoor = roomDoors[0];
        float minDistance = (nearestDoor.transform.position - playerPosition).magnitude;
        foreach (DoorDOGOption door in roomDoors)
        {
            float distance = (door.transform.position - playerPosition).magnitude;
            if (distance < minDistance)
            {
                nearestDoor = door;
                minDistance = distance;
            }
        }

        return (DOGOption)nearestDoor;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerBodyTag)
        {
            NavigationManager.instance.currentPlayerRoom = this;
        }
        //If a vehicle enters our area, we make him our child
        //if (other.gameObject.GetComponent<VehicleDOGOption>() != null)
        //{
        //    other.gameObject.transform.SetParent(transform);
        //}
    }
}
