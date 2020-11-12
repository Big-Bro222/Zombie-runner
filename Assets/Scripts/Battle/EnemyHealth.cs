using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;
    [SerializeField] GameObject miniMapIcon;
    bool isDead = false;
    Collider[] colliders;
    private void Awake()
    {
        //colliders = GetComponentsInChildren<Collider>();
        //foreach(Collider collider in colliders)
        //{
        //    collider.enabled = false;
        //}
        //GetComponent<CapsuleCollider>().enabled = true;

    }
    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(float damage)
    {
        BroadcastMessage("OnDamageTaken");
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //foreach (Collider collider in colliders)
        //{
        //    collider.enabled = true;
        //}
        GetComponent<CapsuleCollider>().enabled = false;
        if (isDead) return;
        isDead = true;
        miniMapIcon.SetActive(false);
        GetComponent<Animator>().SetTrigger("die");
        Transform zombie = transform.GetChild(0);
        GetComponent<Animator>().enabled = false;
        Destroy(gameObject, 15);
    }
}
