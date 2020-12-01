using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    //Default weapon;

    [Serializable]
    public class Weaponcollection
    {
        public string name;
        public Transform weapon;
    }
    public Weapon Gun;
    public ThrowWeapon throwWeapon;
    public Transform disabledWeapon;
    public List<Weaponcollection> weaponcollections;
    [SerializeField] Transform weaponCollection;

    private void Awake()
    {
        string weaponName = GlobalModel.Instance.startWeapon;
        Gun = weaponCollection.Find(weaponName).GetComponent<Weapon>();
        Gun.transform.parent = transform;
        for(int i = 0; i < weaponCollection.childCount; i++)
        {
            weaponCollection.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void PickUpWeapon(Transform weapon)
    {
        if (weapon.GetComponent<WeaponPickUp>())
        {
            string weaponName = weapon.GetChild(0).name;
            if (Gun != null)
            {
                SwitchWeapon(Gun.name, weaponName);
            }
            else
            {
                SwitchWeapon(weaponName);
            }

        }
        else if(weapon.GetComponent<ThrowWeapon>())
        {
            if (throwWeapon != null)
            {
                //ThrowCurrentGunGameobject;
            }
            else
            {
                throwWeapon = weapon.GetComponent<ThrowWeapon>();
                weapon.transform.parent = transform;
                //Add into weapon slot
            }
        }

        
    }
    private void SwitchWeapon(string futureWeaponName)
    {

        Transform futureWeapon=transform;
        foreach(Weaponcollection weaponcollection in weaponcollections)
        {
            if (weaponcollection.name.Equals(futureWeaponName))
            {
                futureWeapon = weaponcollection.weapon;
            }
        };
        if (futureWeapon == transform)
        {
            Debug.LogError("Cannot find the weapon: " + futureWeaponName);
            return;
        }
        futureWeapon.gameObject.SetActive(true);
        futureWeapon.parent = transform;
        Gun = futureWeapon.GetComponent<Weapon>();
    }

    private void SwitchWeapon(string previousWeaponName,string futureWeaponName)
    {
        if (previousWeaponName.Equals(futureWeaponName))
        {
            Gun.AmmoReset();
            Gun.DisplayAmmo();
        }
        else
        {
            //find previous weapon
            Transform previousWeapon = transform.Find(previousWeaponName);
            int siblingIndex = previousWeapon.GetSiblingIndex();

            //find futureWeapon
            Transform futureWeapon = transform;
            foreach (Weaponcollection weaponcollection in weaponcollections)
            {
                if (weaponcollection.name.Equals(futureWeaponName))
                {
                    Debug.Log("Find");
                    futureWeapon = weaponcollection.weapon;
                }
            };
            Debug.Log(futureWeapon.name);

            if (futureWeapon == transform)
            {
                Debug.LogError("Cannot find the weapon: " + futureWeaponName);
                return;
            }
            futureWeapon.gameObject.SetActive(true);
           
            //swap weapons
            futureWeapon.parent = transform;
            previousWeapon.parent = weaponCollection;
            previousWeapon.gameObject.SetActive(false);

            futureWeapon.SetSiblingIndex(siblingIndex);
            previousWeapon.GetComponent<Weapon>().AmmoReset();

            Gun = futureWeapon.GetComponent<Weapon>();
        }
        



    }
}
