using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : Pickup
{
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] float SelfDestroyTime;

    private void Start()
    {
        Destroy(gameObject, SelfDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.GetComponent<AudioSource>().PlayOneShot(pickUpSFX);
            other.GetComponentInChildren<WeaponSlot>().PickUpWeapon(transform);
            Destroy(gameObject);

        }
    }
}
