using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{

    private enum ZombieType { Normal, Offensive, Crawl }

    [SerializeField] ZombieType zombieType;
    [SerializeField] float warningRange = 2.5f;
    [SerializeField] AudioClip[] warningAudioClip;
    [SerializeField] float stopTime = 0.2f;
    [SerializeField] float turnSpeed = 5f;
    [SerializeField] float CullingDistance=100f;
    public Transform AssignedTarget;
    NavMeshAgent navMeshAgent;
    EnemyHealth health;
    PlayerHealth[] targets;
    Animator animator;
    AudioSource warningAudioSource;
    Transform cloestTarget;
    CapsuleCollider bodycollider;
    SkinnedMeshRenderer skinnedMeshRenderer;
    EnemyAttack enemyAttack;
    float distanceToTarget;
    bool isWalkable;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        targets = FindObjectsOfType<PlayerHealth>();
        animator = GetComponent<Animator>();
        warningAudioSource = GetComponent<AudioSource>();
        enemyAttack = GetComponent<EnemyAttack>();
        distanceToTarget = 0;
        isWalkable = true;
        bodycollider = GetComponent<CapsuleCollider>();
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        NavMeshSetDestination(AssignedTarget.position,8.0f);
        cloestTarget = AssignedTarget;
    }


    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(AssignedTarget.position, transform.position);
        CheckCloestTarget();
        if (navMeshAgent.isActiveAndEnabled)
        {
            EngageTarget();
        }


    }


    public void OnDamageTaken()
    {
        isWalkable = false;
        animator.SetTrigger("damage");
        navMeshAgent.isStopped = true;
        Invoke("ResumeWalk", stopTime);
    }

    private void ResumeWalk()
    {
        isWalkable = true;
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
        if(Vector3.Distance(transform.position, Camera.main.transform.position)> CullingDistance)
        {
            if (skinnedMeshRenderer.enabled)
            {
                skinnedMeshRenderer.enabled = false;
            }
        }
        else
        {
            if (!skinnedMeshRenderer.enabled)
            {
                skinnedMeshRenderer.enabled = true;
            }
            
        }
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
    }

    private void NavMeshSetDestination(Vector3 position,float stopdistance)
    {
        navMeshAgent.SetDestination(position);
        navMeshAgent.stoppingDistance = stopdistance;

    }
    public void CheckCloestTarget()
    {

        //Check the cloest Target and setDestination
        Transform closetPlayer=null;
        float stopDistance = 2;
        foreach (PlayerHealth player in targets)
        {
            float p_ZDistance = Vector3.Distance(player.transform.position, transform.position);
            if (p_ZDistance < distanceToTarget)
            {
                closetPlayer = player.transform;
            }
        }

        if (closetPlayer!= null)
        {
            cloestTarget = closetPlayer;
            stopDistance = 2;
        }
        else
        {
            cloestTarget = AssignedTarget;
            stopDistance = 7.0f;
        }
        NavMeshSetDestination(cloestTarget.position,stopDistance);

    }


    private void EngageTarget()
    {
        if (!isWalkable)
        {
            return;
        }
        CheckWarningDistance();
        FaceTarget();
        float distanceZtoTarget = Vector3.Distance(transform.position, cloestTarget.position);
        if (distanceZtoTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceZtoTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void FaceTarget()
    {
        Vector3 direction = (cloestTarget.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
    private void AttackTarget()
    {
        animator.SetBool("attack", true);
        enemyAttack.SetTarget(cloestTarget);
        //enemyAttack.AttackHitEvent();
    }

}
