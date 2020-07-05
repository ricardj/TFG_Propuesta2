using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExtractionScript : MonoBehaviour
{
    public string playerTag;

    public void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == playerTag)
        {
            SceneManager.LoadScene("LevelCompleted");
        }
    }
}
