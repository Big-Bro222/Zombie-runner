using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    Transform gameTarget;
    [SerializeField] int damage = 40;

    void Start()
    {

    }

    public void SetTarget(Transform target)
    {
        gameTarget = target;
    }

    public void AttackHitEvent()
    {
        if (gameTarget == null) return;
        if (gameTarget.GetComponent<PlayerHealth>())
        {
            gameTarget.GetComponent<PlayerHealth>().TakeDamage(damage);
            gameTarget.GetComponent<DisplayDamage>().ShowDamageImpact();
        }
        else
        {
            gameTarget.GetComponent<PropsHealthUpdate>().TakeDamage(damage);
        }
    }

}
