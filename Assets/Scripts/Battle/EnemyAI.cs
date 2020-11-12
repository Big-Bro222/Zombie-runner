using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum ZombieType {Normal, Offensive, Crawl}

    [SerializeField] ZombieType zombieType;
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float warningRange = 2.5f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float stopTime = 0.2f;
    [SerializeField] AudioClip[] warningAudioClip;
    NavMeshAgent navMeshAgent;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;
    EnemyHealth health;
    Transform target;
    Animator animator;
    AudioSource warningAudioSource;

    bool isWalkable = true;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        target = FindObjectOfType<PlayerHealth>().transform;
        animator = GetComponent<Animator>();
        warningAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (health.IsDead())
        {
            enabled = false;
            navMeshAgent.enabled = false;
            return;
        }
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked&&navMeshAgent.isActiveAndEnabled)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
        }
        
        if (distanceToTarget > chaseRange)
        {
            navMeshAgent.isStopped = true;
        }

    }
    private void ResumeWalk()
    {
        isWalkable = true;
    }
    public void OnDamageTaken()
    {
        isWalkable = false;
        animator.SetTrigger("damage");
        navMeshAgent.isStopped = true;
        isProvoked = true; 
        Invoke("ResumeWalk", stopTime);
    }

    private void EngageTarget()
    {
        if (!isWalkable)
        {
            return;
        }
        CheckWarningDistance();
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void CheckWarningDistance()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < warningRange && !warningAudioSource.isPlaying)
        {
            int clipIndex = (int)Random.Range(0, warningAudioClip.Length - 1);
            warningAudioSource.clip = warningAudioClip[clipIndex];
            warningAudioSource.Play();
        }
        else if (Vector3.Distance(transform.position, Camera.main.transform.position) > warningRange && warningAudioSource.isPlaying)
        {
            warningAudioSource.Stop();
        }
    }

    private void ChaseTarget()
    {
        animator.SetBool("attack", false);
        animator.SetTrigger("move");
        if (zombieType == ZombieType.Normal)
        {
            EnableMoving();
        }
    }

    public void EnableMoving()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        //Debug.Log(distanceToTarget);
        animator.SetBool("attack", true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
