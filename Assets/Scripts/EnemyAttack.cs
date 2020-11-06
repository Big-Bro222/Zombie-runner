using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 40f;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        Debug.Log("getCalled");
        if (target == null) return;
        target.TakeDamage(damage);
        target.GetComponent<DisplayDamage>().ShowDamageImpact();
    }

}
