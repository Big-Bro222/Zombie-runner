using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    [SerializeField] AudioClip weaponSwitchSFX;

    void Start()
    {
        SetWeaponActive();
    }

    void Update()
    {
        int previousWeapon = currentWeapon;

        ProcessKeyInput();
        ProcessScrollWheel();
    }

    private void ProcessScrollWheel()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            SwitchToNextWeapon();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            SwitchToPreviousWeapon();
        }
    }

    public void SwitchToPreviousWeapon()
    {
        GetComponentInParent<AudioSource>().PlayOneShot(weaponSwitchSFX);
        if (currentWeapon <= 0)
        {
            currentWeapon = transform.childCount - 1;
        }
        else
        {
            currentWeapon--;
        }
        SetWeaponActive();

    }

    public void SwitchToNextWeapon()
    {
        GetComponentInParent<AudioSource>().PlayOneShot(weaponSwitchSFX);
        if (currentWeapon >= transform.childCount - 1)
        {
            currentWeapon = 0;
        }
        else
        {
            currentWeapon++;
        }
        SetWeaponActive();
    }

    private void ProcessKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
    }

    private void SetWeaponActive()
    {
        int weaponIndex = 0;

        foreach (Transform weapon in transform)
        {
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }

    }

}
