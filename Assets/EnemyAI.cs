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

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent= GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        distanceToTarget=Vector3.Distance(target.position,transform.position);
        if(distanceToTarget<= detectablerange){
             navMeshAgent.SetDestination(target.position);
        }
        
    }

     void OnDrawGizmosSelected() {
            Gizmos.color= new Color(1,1,0,0.75F);
            Gizmos.DrawWireSphere(transform.position,detectablerange);
        }
}
