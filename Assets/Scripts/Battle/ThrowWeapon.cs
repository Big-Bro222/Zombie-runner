using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ThrowWeapon : MonoBehaviour
{
    public float throwSpeed;
    [SerializeField] TextMeshProUGUI ammo;
    [SerializeField] TextMeshProUGUI clips;
    [SerializeField] GameObject throwPrefab;
    [SerializeField] Transform ThrowPoint;
    [SerializeField] Rigidbody playerBodyRig;
    public int ammoAmount;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    public void ReloadAmmo(int ammoAmount)
    {
        this.ammoAmount += ammoAmount;
        UpdateAmmoUI();
    }

    private void OnEnable()
    {
        UpdateAmmoUI();
    }

    private void UpdateAmmoUI()
    {
        clips.text = "";
        ammo.text = ammoAmount.ToString();
    }

    private void DecreaseAmmo()
    {
        ammoAmount--;
    }

    private void Throw()
    {
        Vector3 Speed = ThrowPoint.transform.forward * throwSpeed + playerBodyRig.velocity;
        GameObject instancePrefab = Instantiate(throwPrefab,ThrowPoint.position,ThrowPoint.rotation);
        instancePrefab.GetComponent<Rigidbody>().velocity = Speed;
        DecreaseAmmo();
        UpdateAmmoUI();
        if (ammoAmount == 0)
        {
            GetComponentInParent<WeaponSwitcher>().SwitchToNextWeapon();
            Destroy(gameObject);
        }
    }

}
