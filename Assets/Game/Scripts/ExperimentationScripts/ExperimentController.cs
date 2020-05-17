using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ExperimentController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent vehicle;
    public NavMeshAgent character;

    NavMeshAgent currentNavMeshAgent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                currentNavMeshAgent.SetDestination(hit.point);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            currentNavMeshAgent = vehicle;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentNavMeshAgent = character;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //We parent or unparent the charachter to the vehicle
            character.gameObject.transform.SetParent(vehicle.gameObject.transform);


        }
    }
}
