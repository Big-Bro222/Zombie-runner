using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : Pickup
{
    [SerializeField] private int HealthPoint;
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] float SelfDestroyTime;
    void Start()
    {
        Destroy(gameObject, SelfDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<AudioSource>().PlayOneShot(pickUpSFX);
            other.GetComponentInChildren<PlayerHealth>().Heal(HealthPoint);
            Destroy(gameObject);

        }
    }
}
