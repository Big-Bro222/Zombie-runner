using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyAI : MonoBehaviour
{
    
    [SerializeField] Transform target;
    [SerializeField] float detectablerange=5f;
    NavMeshAgent navMeshAgent;
    float distanceToTarget=Mathf.Infinity;
    bool isProvoke=false;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent= GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {

        distanceToTarget=Vector3.Distance(target.position,transform.position);
        if(isProvoke)
        {

            EngageTarget();
        }
        else if (distanceToTarget<= detectablerange){
            isProvoke=true;
        }
        
    }

    private void EngageTarget()
    {
        if(distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget<= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private  void ChaseTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("move");
        navMeshAgent.SetDestination(target.position);
    }

    private  void AttackTarget()
    {
        GetComponent<Animator>().SetBool("Attack",true);
        Debug.Log( name+ "has seeked and is destroying"+target.name);
    }


    void OnDrawGizmosSelected() {
            Gizmos.color= new Color(1,1,0,0.75F);
            Gizmos.DrawWireSphere(transform.position,detectablerange);
        }
    
}

