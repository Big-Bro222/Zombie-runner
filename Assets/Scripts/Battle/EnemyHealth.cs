using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int Health = 100;
    [SerializeField] GameObject miniMapIcon;
    bool isDead = false;
    private int hitPoints;
    Collider[] colliders;
    private void Awake()
    {
        hitPoints = Health;
        colliders = GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Collider>().enabled = true;
    }
    public bool IsDead()
    {
        return isDead;
    }

    public void TakeDamage(int damage)
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
        //Update score
        ScoreSystem.Instance.AddScore(Health);
        ScoreView.Instance.StartCoroutine(ScoreView.Instance.UpdateCurrentScore());
        foreach (Collider collider in colliders)
        {
            collider.enabled = true;
        }
        GetComponent<CapsuleCollider>().enabled = false;
        if (isDead) return;
        isDead = true;
        miniMapIcon.SetActive(false);
        GetComponent<Animator>().SetTrigger("die");
        Transform zombie = transform.GetChild(0);
        GetComponent<Animator>().enabled = false;
        GetComponent<ZombieAI>().enabled = false;
        Invoke("DisableGameObject", 2);
    }

    private void DisableGameObject()
    {
        gameObject.SetActive(false);
    }
}
