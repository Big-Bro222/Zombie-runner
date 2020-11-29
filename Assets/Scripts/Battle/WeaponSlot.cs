using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    //Default weapon;
    public Weapon Gun;
    public ThrowWeapon throwWeapon;
    public Transform disabledWeapon;
    [SerializeField] Transform weaponCollection;

    private void Awake()
    {
        string weaponName = GlobalModel.Instance.startWeapon;
        Gun = weaponCollection.Find(weaponName).GetComponent<Weapon>();
        Gun.transform.parent = transform;
        foreach(Transform weapon in weaponCollection)
        {
            weapon.parent = disabledWeapon;
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
            Gun = weapon.GetComponent<Weapon>();

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
        Transform futureWeapon = disabledWeapon.Find(futureWeaponName);
        futureWeapon.parent = transform;

    }

    private void SwitchWeapon(string previousWeaponName,string futureWeaponName)
    {
        Debug.Log(previousWeaponName+" and after "+ futureWeaponName);
        Transform previousWeapon = transform.Find(previousWeaponName);
        int siblingIndex = previousWeapon.GetSiblingIndex();


        Transform futureWeapon = disabledWeapon.Find(futureWeaponName);

        previousWeapon.parent = disabledWeapon;
        futureWeapon.SetSiblingIndex(siblingIndex);
        previousWeapon.GetComponent<Weapon>().AmmoReset();

        futureWeapon.parent = transform;
    }
}
