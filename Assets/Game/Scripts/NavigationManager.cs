using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationManager : MonoBehaviour
{

    //Singleton stuff
    public static NavigationManager instance;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public DOGInterface playerDogInterface;

    public OutdoorDOGOption currentPlayerOutdoor;
    public OutdoorDOGOption previousCurrentPlayerOutdoorValue;
    public BuildingDOGOption currentPlayerRoom;
    private BuildingDOGOption previousCurrentPlayerRoomValue;

    List<BuildingDOGOption> discoveredRooms;

    void Start()
    {
        if (playerDogInterface == null) Debug.LogError("Error: missing DOG interface");
        previousCurrentPlayerRoomValue = currentPlayerRoom;
        discoveredRooms = new List<BuildingDOGOption>();
        discoveredRooms.Add(currentPlayerRoom);

        previousCurrentPlayerOutdoorValue = currentPlayerOutdoor;
    }

    void Update()
    {
        //TODO: check if the variable has changed.
        if (currentPlayerRoom != previousCurrentPlayerRoomValue)
        {
            previousCurrentPlayerRoomValue = currentPlayerRoom;
            if (!discoveredRooms.Contains(currentPlayerRoom)) discoveredRooms.Add(currentPlayerRoom);
            playerDogInterface.updateDogInterface();
        }
        if (currentPlayerOutdoor != previousCurrentPlayerOutdoorValue)
        {
            previousCurrentPlayerOutdoorValue = currentPlayerOutdoor;
            discoveredRooms.Clear();
            playerDogInterface.updateDogInterface();
        }

    }

    public List<DOGOption> loadRoomDogOptions()
    {

        List<DOGOption> currentRoomDogOptions = new List<DOGOption>();
        DOGOption[] dogOptions = currentPlayerRoom.gameObject.GetComponentsInChildren<DOGOption>();

        //we dont take the first because is the room itself
        bool first = true;
        foreach (DOGOption roomDogOption in dogOptions)
        {
            if (first) first = false;
            else
                currentRoomDogOptions.Add(roomDogOption);
        }
        return currentRoomDogOptions;
    }

    public List<BuildingDOGOption> loadBuildingDogOptions()
    {
        //TODO: depending of the room, we have to return some doors or others.
        //We retrieve the doors of the current player building.
        return discoveredRooms;
    }

    public List<OutdoorDOGOption> loadOutdoorDogOptions()
    {

        List<OutdoorDOGOption> returnList = new List<OutdoorDOGOption>(currentPlayerOutdoor.getAccessibleOutdoors());

        //If the player has completed the object stuff, we add the helicopter
        if (GMLevel.instance.objectInterface.GetComponent<PlayerObjectInterface>().allCodesPicked)
        {
            returnList.Add(GMLevel.instance.extractionDOGOption);
        }

        return returnList;
    }

    public void vehicleUpdate()
    {
        playerDogInterface.updateDogInterface();
    }
}

//Some thoughts

//checks where is the player  and wich options he has access to.
//Depending of where the player is, has acces to diferent options in diferent scopes.
//Who decides which options at which moment?
//We should decide that from the unity editor
//Depending on the room you are you have one room options, one building options and one outdoor options.
//If youre in Starter Room you have the following options:
/*
    Room options:
        -Panel A
        _Coberture 1
        -Coverture 2
        -Coberure 3
    Building options:
        -Cross room
    Outdoor options:
        -No options.

If you choose to go to cross room you then have the following options:

    Room Options:
        -Panel A
        -Panel B
    Building options:
        -Starter room
        -Laboratory
        -Warehouse
        -Exit
    Outdoor options:
        -No options

If you choose to go the exit you have:
    Room options:
        -Go to Car 1
        -Go to car 2
        -Go to spaceship
    Building options:
        -Enter Cross room.
        (-Enter through laboratory for example?)
    Outdoor options:
        -Go to Base A
        -Go to Base B
        -Go to BAse C

if you choose to go to base B then you should have:
    Room options:
        -Go to car
        -Go to Panel
    Building options:
        -(Enter through warehous or other)

    Outdoor:
        -Go to whatever place

if you choose to enter the warehouse:
    Room options:
        -Warehouse content options
    Building options:
        -Warehouse surrounding options.
    Outdoor:
        -No options

*/
