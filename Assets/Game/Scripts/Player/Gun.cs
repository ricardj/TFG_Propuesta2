using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Gun sounds
    public AudioClip shootSound;
    public AudioClip aimSound;
    AudioSource[] audioSource;

    //Gun parameters
    public float damage = 10f;

    public float range = 100f;

    public Camera mainCamera;

    public GameObject gunRayPrefab;

    //Aiming parameters
    IEnumerator aimCoroutine;
    bool aimActivated = false;
    public float aimingPeriod = 0.2f;
    public float maxAimingDistance = 10.0f;
    public float maxAimingPitch = 3f;
    public float minAimingPitch = 1f;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponents<AudioSource>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            audioSource[0].PlayOneShot(shootSound);
            Shoot();
        }

        if (Input.GetMouseButton(1))
        {
            aimActivated = true;
            if (aimCoroutine == null)
            {
                aimCoroutine = Aim();
                StartCoroutine(aimCoroutine);
            }
        }
        else
        {
            aimActivated = false;
            aimCoroutine = null;
        }
    }

    public IEnumerator Aim()
    {
        while (aimActivated)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position,
                                mainCamera.transform.forward,
                                out hit))
            {

                float distance = (transform.position - hit.point).magnitude;
                if (distance < maxAimingDistance)
                {
                    float aimPitch = maxAimingPitch - (distance / maxAimingDistance) * minAimingPitch;
                    playAimWithPitch(aimPitch);
                }
                else
                {
                    playAimWithPitch(1.0f);
                }
            }
            else
            {
                playAimWithPitch(1f);
            }
            yield return new WaitForSeconds(aimingPeriod);
        }
    }

    public void playAimWithPitch(float pitch)
    {
        audioSource[1].pitch = pitch;
        audioSource[1].PlayOneShot(aimSound);
    }
    public void Shoot()
    {
        RaycastHit hit;

        if (Physics.Raycast(mainCamera.transform.position,
                            mainCamera.transform.forward,
                            out hit))
        {
            Enemy enemy = hit.transform.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {

                enemy.takeDamage(damage);
            }

            //Si o si mostramos el rayo
            GameObject instantiatedGunRay = Instantiate(gunRayPrefab);
            instantiatedGunRay.GetComponent<GunRay>().SetRay(mainCamera.transform.position, hit.point);
        }
    }
}
