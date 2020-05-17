using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DOGInterface : MonoBehaviour
{

    bool activated = false;

    //Menu sounds
    [Header("DOG sounds")]
    public AudioClip activationSound;
    public AudioClip deactivationSound;
    public AudioClip menuSound;
    public AudioClip changeOptionSound;
    public AudioClip changeScopeSound;

    public AudioClip enterDoorSound;

    public AudioClip roomScopeSound, buildingScopeSound, outdoorScopeSound;
    public AudioClip errorSound; //For moments where there's no options avaliable.
    public AudioClip dogUpdatedSound;

    AudioSource audioSource;

    [Header("DOG controls")]
    //Keys for interacting with the dog interface
    public KeyCode activationKey = KeyCode.Space;

    public KeyCode upMoveKey = KeyCode.W;
    public KeyCode downMoveKey = KeyCode.S;

    public KeyCode abortKey = KeyCode.Q;
    public KeyCode forwardKey = KeyCode.D;
    public KeyCode backKey = KeyCode.A;
    public KeyCode repeatOptionKey = KeyCode.R;
    public KeyCode enterDoorKey = KeyCode.E;


    [Header("DOG modes and its options")]
    public List<DOGOption> roomDogOptions;
    public List<DOGOption> buildingDogOptions;
    public List<DOGOption> outdoorDogOptions;
    DOGOption currentDogOption;

    public enum DogOptionScope { Room, Building, Outdoor };
    public DogOptionScope currentDogOptionScope = DogOptionScope.Room;
    int roomDogOptionIndex;
    int buildingDogOptionIndex;
    int outdoorDogOptionIndex;

    [Header("Body the DOG will move")]
    //Elements to which the dog has acces and action to
    public NavMeshAgent playerNavMeshAgent;

    //Dog positioning options
    float dogRadius;
    float dogHeight;


    //If the player is mounted in a vehicle
    bool isVehicle = false;
    public VehicleDOGOption vehicle;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        updateDogInterface();

        roomDogOptionIndex = 0;
        currentDogOption = roomDogOptions[roomDogOptionIndex];

        buildingDogOptionIndex = 0;
        outdoorDogOptionIndex = 0;

        dogRadius = transform.localPosition.magnitude;
        dogHeight = transform.position.y;
    }

    public bool interrupt = false;
    public DOGOption dogOptionPlayer;
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(activationKey))
        {
            showMenu();

            currentDogOptionScope = DogOptionScope.Room;

            showCurrentOption();

            dogOptionPlayer = currentDogOption;
            activated = true;
        }

        //activated = Input.GetKey(activationKey) ? true : false;

        if (activated == true)
        {
            if (Input.GetKeyDown(upMoveKey)) moveUp();
            if (Input.GetKeyDown(downMoveKey)) moveDown();
            if (Input.GetKeyDown(forwardKey)) moveRight();
            if (Input.GetKeyDown(backKey)) moveLeft();

            if (Input.GetKeyDown(repeatOptionKey)) showCurrentOption();

            if (Input.GetKeyDown(enterDoorKey))
            {
                enterDoor();
                interrupt = true;
            }
            if (Input.GetKeyDown(abortKey))
            {
                currentDogOption = dogOptionPlayer;
                audioSource.PlayOneShot(errorSound);
                interrupt = true;
            }
        }

        if (Input.GetKeyUp(activationKey) && !interrupt && activated)
        {
            activated = false;
            showCurrentOption();
            activateOption();
            hideMenu();
        }

        if (interrupt)
        {
            activated = false;
            interrupt = false;
            showCurrentOption();
            hideMenu();
        }

        orientateDogDirection();
    }

    //SHOW AND HIDE STUFF
    public void showCurrentOption()
    {
        //Debug.Log("Show current option");
        audioSource.clip = currentDogOption.nameSound;
        if (currentDogOption == null) audioSource.clip = errorSound;
        audioSource.Play();
    }

    public void showCurrentScope()
    {
        switch (currentDogOptionScope)
        {
            case DogOptionScope.Room:
                audioSource.clip = roomScopeSound;
                break;
            case DogOptionScope.Building:
                audioSource.clip = buildingScopeSound;
                break;
            case DogOptionScope.Outdoor:
                audioSource.clip = outdoorScopeSound;
                break;
        }
        audioSource.Play();
    }

    public void showMenu()
    {
        audioSource.PlayOneShot(activationSound);
    }

    public void hideMenu()
    {
        audioSource.PlayOneShot(deactivationSound);
    }


    //MOVE UP AND DOWN STUFF
    public void moveUp()
    {
        move(true);
    }

    public void moveDown()
    {
        move(false);
    }

    public int moveUpIndex(int index, List<DOGOption> currentDogOptionList)
    {
        return index > 0 ? index - 1 : currentDogOptionList.Count - 1;
    }

    public int moveDownIndex(int index, List<DOGOption> currentDogOptionList)
    {
        return index < currentDogOptionList.Count - 1 ? index + 1 : 0;
    }

    public void move(bool isUp)
    {
        switch (currentDogOptionScope)
        {
            case DogOptionScope.Room:
                roomDogOptionIndex = isUp ? moveUpIndex(roomDogOptionIndex, roomDogOptions) : moveDownIndex(roomDogOptionIndex, roomDogOptions);
                currentDogOption = roomDogOptions[roomDogOptionIndex];
                break;
            case DogOptionScope.Building:
                buildingDogOptionIndex = isUp ? moveUpIndex(buildingDogOptionIndex, buildingDogOptions) : moveDownIndex(buildingDogOptionIndex, buildingDogOptions);
                currentDogOption = buildingDogOptions[buildingDogOptionIndex];
                break;
            case DogOptionScope.Outdoor:
                outdoorDogOptionIndex = isUp ? moveUpIndex(outdoorDogOptionIndex, outdoorDogOptions) : moveDownIndex(outdoorDogOptionIndex, outdoorDogOptions);
                currentDogOption = outdoorDogOptions[outdoorDogOptionIndex];
                break;
        }

        audioSource.PlayOneShot(changeOptionSound);
        showCurrentOption();
    }




    //MOVE LEFT AND RIGHT STUFF
    public void moveRight()
    {
        switch (currentDogOptionScope)
        {
            case DogOptionScope.Room:
                currentDogOptionScope = DogOptionScope.Building;
                break;
            case DogOptionScope.Building:
                currentDogOptionScope = DogOptionScope.Outdoor;
                break;
            case DogOptionScope.Outdoor:
                currentDogOptionScope = DogOptionScope.Room;
                break;
        }
        audioSource.PlayOneShot(changeScopeSound);
        showCurrentScope();
    }
    public void moveLeft()
    {
        switch (currentDogOptionScope)
        {
            case DogOptionScope.Room:
                currentDogOptionScope = DogOptionScope.Outdoor;
                break;
            case DogOptionScope.Building:
                currentDogOptionScope = DogOptionScope.Room;
                break;
            case DogOptionScope.Outdoor:
                currentDogOptionScope = DogOptionScope.Building;
                break;
        }
        audioSource.PlayOneShot(changeScopeSound);
        showCurrentScope();
    }





    //OTHER INTERFACE ACTIONS
    public void activateOption()
    {
        if (currentDogOption != null)
        {
            switch (currentDogOptionScope)
            {
                case DogOptionScope.Room:

                    if (!isVehicle)
                    {
                        playerNavMeshAgent.SetDestination(currentDogOption.position);
                    }
                    else
                    {
                        audioSource.PlayOneShot(errorSound);
                    }
                    break;

                case DogOptionScope.Building:
                    BuildingDOGOption building = currentDogOption as BuildingDOGOption;
                    if (!isVehicle)
                    {
                        playerNavMeshAgent.SetDestination(building.getNearestDoor().position);
                    }
                    else
                    {
                        audioSource.PlayOneShot(errorSound);
                    }
                    break;

                case DogOptionScope.Outdoor:
                    OutdoorDOGOption outdoor = currentDogOption as OutdoorDOGOption;
                    if (!isVehicle)
                    {
                        playerNavMeshAgent.SetDestination(outdoor.enterPoint.position);
                        Debug.Log("Outdoor option activated.");
                    }
                    else
                    {
                        vehicle.goTo(outdoor);
                        Debug.Log("Going with the vehicle to some place.");
                    }

                    break;
            }

        }

    }

    public void enterDoor()
    {
        audioSource.PlayOneShot(enterDoorSound);

        //We check that the current dog option is a door
        DoorDOGOption doorDOGOption = currentDogOption as DoorDOGOption;
        if (doorDOGOption != null)
        {
            playerNavMeshAgent.SetDestination(doorDOGOption.otherSide.gameObject.transform.position);
            currentDogOption = doorDOGOption.otherSide;
        }

        //Or is the door of a vehicle
        VehicleDOGOption vehicleDOGOption = currentDogOption as VehicleDOGOption;
        if (vehicleDOGOption != null)
        {
            //If the player is in a vehicle
            if (isVehicle)
                getOutVehicle();
            else
                enterVehicle(vehicleDOGOption);

        }
    }

    //Vehicle stuff
    Transform previousParent; //For the parenting unparenting from vehicle stuff.
    public void enterVehicle(VehicleDOGOption vehicleDOGOption)
    {

        isVehicle = true;
        vehicle = vehicleDOGOption;
        previousParent = playerNavMeshAgent.transform.parent;
        playerNavMeshAgent.gameObject.transform.SetParent(vehicle.gameObject.transform);
        playerNavMeshAgent.enabled = false;

        vehicle.turnOn();
    }

    public void getOutVehicle()
    {
        isVehicle = false;
        vehicle.turnOff();
        vehicle = null;

        playerNavMeshAgent.enabled = true;
        playerNavMeshAgent.gameObject.transform.SetParent(previousParent);
    }




    //Other stuff
    public void orientateDogDirection()
    {
        Vector3 difference = (currentDogOption.position - playerNavMeshAgent.gameObject.transform.position);
        if (difference.magnitude >= 0) difference = difference.normalized;

        //difference = transform.TransformPoint(difference * dogRadius);
        difference = (difference * dogRadius);
        difference.y = dogHeight;
        transform.position = playerNavMeshAgent.gameObject.transform.position + difference;
    }


    //TODO: put here the methods for external use
    public void updateDogInterface()
    {
        roomDogOptions = NavigationManager.instance.loadRoomDogOptions();
        buildingDogOptions = NavigationManager.instance.loadBuildingDogOptions().ConvertAll<DOGOption>(item => (DOGOption)item);
        outdoorDogOptions = NavigationManager.instance.loadOutdoorDogOptions().ConvertAll<DOGOption>(item => (DOGOption)item);

        if (audioSource != null) audioSource.PlayOneShot(dogUpdatedSound);
    }
}
