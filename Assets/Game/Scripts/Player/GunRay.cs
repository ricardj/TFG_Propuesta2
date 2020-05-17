using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRay : MonoBehaviour
{
    bool rayActive = false;
    public Vector3 startPosition;
    public Vector3 endPosition;

    public float rayDuration = 5f;

    LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetRay(Vector3 startPosition, Vector3 endPosition)
    {
        rayActive = true;
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
        IEnumerator coroutine = waitAndDestroy(rayDuration);
        StartCoroutine(coroutine);
    }

    // every 2 seconds perform the print()
    private IEnumerator waitAndDestroy(float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
        Destroy(gameObject);

    }
}
