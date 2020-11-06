using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] AudioClip shootingSound;




    private AudioSource w_audioSource;

    bool canShoot = true;

    private void OnEnable()
    {
        w_audioSource = GetComponent<AudioSource>();
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();
        if (Input.GetMouseButton(0) && canShoot == true)
        {
            StartCoroutine(Shoot());
        }
    }

    private void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;
        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            w_audioSource.PlayOneShot(shootingSound);
            PlayMuzzleFlash();
            ProcessRaycast();
            ammoSlot.ReduceCurrentAmmo(ammoType);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        canShoot = true;
    }

    private void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    private void ProcessRaycast()
    {
        if (!(ammoType==AmmoType.Shells))
        {
            RaycastHit hit;
            if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                target.TakeDamage(damage);
            }
            else
            {
                return;
            }
        }
        else
        {
            Debug.Log("shotgun");
            ShotGunRaycastGenerator("Up");
            ShotGunRaycastGenerator("Down");
            ShotGunRaycastGenerator("Left");
            ShotGunRaycastGenerator("Right");
            ShotGunRaycastGenerator("Forward");

        }

    }


    private void ShotGunRaycastGenerator (string direction ){
        RaycastHit[] shotGunHits;
        Vector3 Direction=Vector3.zero;
        switch (direction)
        {
            case "Up":
                Direction = FPCamera.transform.forward + 0.15f * FPCamera.transform.up;
                break;
            case "Down":
                Direction = FPCamera.transform.forward - 0.15f * FPCamera.transform.up;
                break;
            case "Left":
                Direction = FPCamera.transform.forward - 0.15f * FPCamera.transform.right;
                break;
            case "Right":
                Direction = FPCamera.transform.forward + 0.15f * FPCamera.transform.right;
                break;
            case "Forward":
                Direction = FPCamera.transform.forward;
                break;
            default:
                break;

        }

        shotGunHits = Physics.RaycastAll(FPCamera.transform.position, Direction, range);

        if (shotGunHits.Length > 0)
        {
            
            CreateHitImpact(shotGunHits);
            foreach (RaycastHit shotHit in shotGunHits)
            {
                EnemyHealth target = shotHit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                Debug.Log(target.name);
                float distanceHitGun = Vector3.Distance(shotHit.point, FPCamera.transform.position);
                //float decayedDamage = (1 - distanceHitGun / range) * damage;
                target.TakeDamage(damage);
            }

        }
    }
    private void CreateHitImpact(RaycastHit hit)
    {
        Debug.Log(hit.collider.name);
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .5f);
    }


    private void CreateHitImpact(RaycastHit[] hits)
    {
        //To do: impact fx
        Debug.Log("Impact FX");
    }
    private void OnDrawGizmos()
    {
        if (ammoType == AmmoType.Shells)
        {
            //Ray ray = new Ray(FPCamera.transform.position, FPCamera.transform.forward + 0.25f * FPCamera.transform.right);
            Debug.DrawRay(FPCamera.transform.position, (FPCamera.transform.forward + 0.15f * FPCamera.transform.right).normalized * range);


            //Ray ray2 = new Ray(FPCamera.transform.position, FPCamera.transform.forward - 0.25f * FPCamera.transform.right);
            Debug.DrawRay(FPCamera.transform.position, (FPCamera.transform.forward - 0.15f * FPCamera.transform.right).normalized * range);

            //Ray ray3 = new Ray(FPCamera.transform.position, FPCamera.transform.forward + 0.25f * FPCamera.transform.up);
            Debug.DrawRay(FPCamera.transform.position, (FPCamera.transform.forward + 0.15f * FPCamera.transform.up).normalized * range);

            //Ray ray4 = new Ray(FPCamera.transform.position, FPCamera.transform.forward - 0.25f * FPCamera.transform.up);
            Debug.DrawRay(FPCamera.transform.position, (FPCamera.transform.forward - 0.15f * FPCamera.transform.up).normalized * range);

            Debug.DrawRay(FPCamera.transform.position, (FPCamera.transform.forward).normalized * range);
        }
    }
}
