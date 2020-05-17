using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSteps : MonoBehaviour
{

    public AudioClip stepAudio;
    public UnityEngine.AI.NavMeshAgent navMeshAgent;

    AudioSource audioSource;

    public float stepDistance = 1.0f;
    Vector3 lastStepPosition;

    public bool stepsActivated = true;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastStepPosition = navMeshAgent.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float difference = (navMeshAgent.gameObject.transform.position - lastStepPosition).magnitude;

        if (difference > stepDistance)
        {
            lastStepPosition = navMeshAgent.gameObject.transform.position;
            if (stepsActivated) playStep();
        }

        if (navMeshAgent.enabled) stepsActivated = true;
        else stepsActivated = false;

    }

    public void playStep()
    {
        audioSource.PlayOneShot(stepAudio);
    }
}
