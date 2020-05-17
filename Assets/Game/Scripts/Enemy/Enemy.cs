using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    //Enemy Audio clips
    public AudioClip deathSound;
    public AudioClip idleSound;
    public AudioClip damagedSound;
    public AudioClip attackSound;

    public float health = 10f;
    public float attack = 10f;
    public float minAttackDistance = 25f;

    //Be careful with that.
    GameObject target;
    PlayerStatus playerStatus;


    NavMeshAgent navMeshAgent;
    AudioSource audioSource;

    public float attackCooldownPeriod = 2f;
    float attackCooldown = 0f;

    public string playerBodyTag = "Player";

    // Start is called before the first frame update
    void Start()
    {
        target = GMLevel.instance.player;
        navMeshAgent = GetComponent<NavMeshAgent>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = idleSound;
        audioSource.Play();
        audioSource.loop = true;

    }

    float targetDistance;
    void Update()
    {
        //Movement stuff
        //They go for the player if he is at a minimum distance
        targetDistance = (target.transform.position - transform.position).magnitude;

        if (targetDistance < minAttackDistance)
            navMeshAgent.SetDestination(target.transform.position);

        //ATtack stuff
        attackCooldown += Time.deltaTime;
        if (attackCooldown > attackCooldownPeriod && playerStatus != null)
        {
            attackCooldown = 0;
            audioSource.PlayOneShot(attackSound);
            playerStatus.hitPlayer(attack);
        }


    }

    public void takeDamage(float damage)
    {
        health -= damage;
        audioSource.PlayOneShot(damagedSound, 1.0f);
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        AudioSource.PlayClipAtPoint(deathSound, transform.position, 1.0f);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == playerBodyTag)
        {
            playerStatus = collider.gameObject.GetComponent<PlayerStatus>();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == playerBodyTag)
        {
            playerStatus = null;
        }
    }
}
