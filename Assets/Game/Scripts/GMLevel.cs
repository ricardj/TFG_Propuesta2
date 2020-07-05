using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMLevel : MonoBehaviour
{
    public GameObject player;
    public GameObject objectInterface;

    public OutdoorDOGOption extractionDOGOption;

    public static GMLevel instance = null;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            Destroy(gameObject);

    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
