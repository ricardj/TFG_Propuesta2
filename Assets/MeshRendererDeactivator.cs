using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRendererDeactivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer[] renderers = GameObject.FindObjectsOfType<MeshRenderer>();
        foreach (MeshRenderer meshRenderer in renderers)
            meshRenderer.enabled = false;
    }


}
