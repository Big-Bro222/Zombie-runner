using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : Pickup
{
    [SerializeField] AudioClip pickUpSFX;
    [SerializeField] float SelfDestroyTime;
    [SerializeField] ParticleSystem particleSystem;
    private void Start()
    {
        Destroy(gameObject, SelfDestroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("PickWeapons");
            other.GetComponent<AudioSource>().PlayOneShot(pickUpSFX);
            other.GetComponentInChildren<WeaponSlot>().PickUpWeapon(transform);
            gameObject.SetActive(false);
            ObjectsPool.instance.SpawnFromPool("Particle", transform.position);
            Destroy(gameObject);

        }
    }
}
