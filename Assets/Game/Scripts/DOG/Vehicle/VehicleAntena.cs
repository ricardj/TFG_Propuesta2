using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAntena : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider collider)
    {
        Debug.Log("Antena is triggering " + collider.gameObject.name);
        if (collider.gameObject.GetComponent<BuildingDOGOption>() != null)
        {
            //We make our father son of another father
            transform.parent.SetParent(collider.gameObject.transform);
            NavigationManager.instance.vehicleUpdate();
        }
    }
}
