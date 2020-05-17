using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float enemySpawningPeriod = 3f;
    public float totalEnemiesToSpawn = 5f;

    public float spawningRadius = 5f;

    float periodCounter = 0;
    float enemyCounter = 0;

    GameObject target;
    public float minDistanceToSpawn = 40f;

    // Start is called before the first frame update
    void Start()
    {
        target = GMLevel.instance.player;
    }

    float playerDistance;
    void Update()
    {
        periodCounter += Time.deltaTime;

        //We spawn only if the player is certainly near from us.
        playerDistance = (target.transform.position - transform.position).magnitude;
        if (playerDistance < minDistanceToSpawn)
        {
            if (periodCounter > enemySpawningPeriod && enemyCounter < totalEnemiesToSpawn)
            {
                periodCounter = 0;
                //Debug.Log(transform.position);

                Vector3 offset = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                offset = offset.normalized * Random.Range(1f, spawningRadius);

                Instantiate(enemyPrefab, transform.position + offset, Quaternion.identity, null);
                enemyCounter++;
            }
        }

    }
}
