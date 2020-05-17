using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager instance = null;

    public List<Object> allSceneObjects;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

        allSceneObjects = new List<Object>();

        Object[] objects = FindObjectsOfType<Object>();
        foreach (var item in objects)
        {
            allSceneObjects.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Object pickUpNearestAvaliableObject()
    {
        Vector3 playerPosition = GMLevel.instance.player.transform.position;

        //We iterate all over the registered objects to find the one that is avaliable
        Object returnObject = null;
        foreach (var item in allSceneObjects)
        {
            if ((item.transform.position - playerPosition).magnitude < item.minPickUpDistance)
            {
                returnObject = item;
            }
        }

        if (returnObject != null) returnObject.pickUp();

        return returnObject;
    }
}
