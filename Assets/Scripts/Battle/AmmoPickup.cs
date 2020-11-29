using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : Pickup
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
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
            if (ammoType == AmmoType.Bullet)
            {
                other.GetComponentInChildren<WeaponSlot>().Gun.ammo.IncreaseCurrentAmmo(ammoAmount);
                other.GetComponentInChildren<WeaponSlot>().Gun.DisplayAmmo();
            }
            else if (ammoType == AmmoType.Throwable)
            {
                other.GetComponentInChildren<WeaponSlot>().throwWeapon.ReloadAmmo(ammoAmount);
            }
            Destroy(gameObject);

        }
    }
}
