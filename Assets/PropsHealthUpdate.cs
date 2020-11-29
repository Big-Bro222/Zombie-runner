using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PropsHealthUpdate : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] GameObject HealthUI;
    [SerializeField] DeathHandler deathHandler;
    [SerializeField] GameObject image;
    private int remainHealth;

    private bool TestModeOn;

    private int defaultColliderNum;
    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        defaultColliderNum = hitColliders.Length;

        remainHealth = health;
        UpdatePropHealthUI("1.00");
        TestModeOn = false;
        InvokeRepeating("ExplosionDamage", 0, 1);
    }
    public void TakeDamage(int point)
    {
        remainHealth -= point;
        if (remainHealth <= 0&&!TestModeOn)
        {
            Debug.Log("you are dead");
            UpdatePropHealthUI("0");
            deathHandler.HandleDeath();
            return;
        }
        float percentage = (float)remainHealth / (float)health;
        UpdatePropHealthUI(percentage.ToString());
    }

    public void UpdatePropHealthUI(string percentage)
    {
        HealthUI.GetComponent<TextMeshPro>().text = percentage;
    }


    void ExplosionDamage()
    {
        Vector3 center = transform.position;
        float radius = 10;
        //detect if any enemy is near the props, show the waypointer marker
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        if(hitColliders.Length == defaultColliderNum){
            return;
        }

        if (hitColliders!=null&&hitColliders.Length!=0)
        {
            if (!image.activeSelf)
            {
                image.SetActive(true);
            }
        }
        else
        {
            if (image.activeSelf)
            {
                image.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TestModeOn = !TestModeOn;
            Debug.Log("TestMode "+ TestModeOn);

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
