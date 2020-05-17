using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class PlayerMovementHelper : MonoBehaviour
{

    //Make the body slave follow him
    public GameObject bodySlave;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (bodySlave != null)
        {
            bodySlave.transform.position = transform.position;
        }
    }



}
