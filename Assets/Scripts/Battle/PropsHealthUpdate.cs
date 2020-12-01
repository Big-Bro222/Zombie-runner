using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PropsHealthUpdate : MonoBehaviour
{
    [SerializeField] int health;
    //[SerializeField] GameObject HealthUI;
    [SerializeField] DeathHandler deathHandler;
    [SerializeField] GameObject warningImage;
    [SerializeField] Image HealthBar;
    private int remainHealth;

    private bool TestModeOn;
    private void Start()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 10);
        remainHealth = health;
        HealthBar.fillAmount = 1;
        TestModeOn = false;
        InvokeRepeating("Propdetection", 0, 1);
    }
    public void TakeDamage(int point)
    {
        remainHealth -= point;
        if (remainHealth <= 0&&!TestModeOn)
        {
            //UpdatePropHealthUI("0");
            HealthBar.fillAmount = 0;
            deathHandler.HandleDeath(DeathReason.BaseMentDestroyed);
            return;
        }
        float percentage = (float)remainHealth / (float)health;
        HealthBar.fillAmount = percentage;

        //UpdatePropHealthUI(percentage.ToString());
    }

    //public void UpdatePropHealthUI(string percentage)
    //{
    //    HealthUI.GetComponent<TextMeshPro>().text = percentage;
    //}


    void Propdetection()
    {
        Vector3 center = transform.position;
        float radius = 10;
        bool detected = false;
        //detect if any enemy is near the props, show the waypointer marker
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach(Collider hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == 10)
            {
                detected = true;
            }
        }

        if (detected)
        {
            if (!warningImage.activeSelf)
            {
                warningImage.SetActive(true);
            }
        }
        else
        {
            if (warningImage.activeSelf)
            {
                warningImage.SetActive(false);
            }
        }        
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    TestModeOn = !TestModeOn;
        //    Debug.Log("TestMode "+ TestModeOn);

        //}
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
