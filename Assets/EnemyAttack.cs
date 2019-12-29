using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float damage= 40f;


    // Update is called once per frame
    public void AttackHitEvent()
    {
        if (target == null) return;
        Debug.Log("bang,bang");
    }
}
