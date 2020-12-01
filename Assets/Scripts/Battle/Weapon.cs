using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class Weapon : MonoBehaviour
{
    
    public Ammo ammo;

    [Serializable]
    public class SoundSettings
    {
        public AudioClip shootingSFX;
        public AudioClip reloadingSFX;
        public AudioClip soldierReloadingSFX;
        public AudioClip emptyClipSFX;
    }
    public SoundSettings soundSettings;
    [SerializeField] Camera FPCamera;
    [SerializeField] AudioSource GlobalAudio;
    [SerializeField] float range = 100f;
    [SerializeField] int damage = 30;
    [SerializeField] ParticleSystem[] muzzleFlashs;
    [SerializeField] GameObject hitEffect;
    //[SerializeField] AmmoType ammoType;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] float reloadTime = 0.5f;

    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] TextMeshProUGUI currentClipText;
    [SerializeField] bool Auto;
    [SerializeField] bool isShotgun;
    [SerializeField] Transform weaponCollection;
    [SerializeField] Image crossHairImage;
    [SerializeField] float recoilStrength;
    [SerializeField] float reloadRotationAngle;
    public CinemachineImpulseSource source;
    private int defaultAmmoAmount;

    private void Start()
    {
        defaultAmmoAmount = ammo.ammoAmount;
        //rotationPivot = transform.GetChild(0);
        //Debug.Log(rotationPivot.name);
        //weaponModel = transform.GetChild(1);
        //Debug.Log(weaponModel.name);
    }


    public void AmmoReset()
    {
        ammo.ammoAmount = defaultAmmoAmount;
        ammo.currentClip = ammo.Clip;

    }

    private AudioSource w_audioSource;

    bool canShoot = true;
    private void OnEnable()
    {
        DisplayAmmo();
        w_audioSource = GetComponent<AudioSource>();
        canShoot = true;
        source.m_ImpulseDefinition.m_AmplitudeGain = recoilStrength;
    }

    void Update()
    {
        if (Auto)
        {
            if (Input.GetMouseButton(0) && canShoot == true)
            {
                StartCoroutine(Shoot());
            }

        }
        else
        {
            if (Input.GetMouseButtonDown(0) && canShoot == true)
            {
                StartCoroutine(Shoot());
            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ammo.GetCurrentAmmo() > 0&&ammo.currentClip!=ammo.Clip)
            {
                StartCoroutine(AmmoReloading());
            }
        }
    }


    IEnumerator AmmoReloading()
    {
        canShoot = false;
        if (ammo.GetCurrentRemain() > 0)
        {
            w_audioSource.PlayOneShot(soundSettings.reloadingSFX);
            GlobalAudio.PlayOneShot(soundSettings.soldierReloadingSFX);
            ammo.Reload();
            //StartCoroutine(ReloadWeapon());
        }
        else
        {
            w_audioSource.PlayOneShot(soundSettings.emptyClipSFX);
        }
        yield return new WaitForSeconds(reloadTime);
        DisplayAmmo();
        canShoot = true;
    }

    //IEnumerator ReloadWeapon()
    //{
    //    Debug.Log("reload");
    //    float rotatedAngle = 0;
    //    float rotatedTime = reloadTime/10;
    //    while(rotatedAngle< reloadRotationAngle)
    //    {
    //        Debug.Log("yes");
    //        rotatedAngle += reloadRotationAngle / 10;
    //        transform.Rotate(transform.right, );
    //        transform.RotateAround(target.transform.position, transform.right, reloadRotationAngle / 10);
    //    }        
    //    yield return new WaitForSeconds(rotatedTime);

    //    Debug.Log("yes");
    //}

    public void DisplayAmmo()
    {
        if (ammo.isInfinity)
        {
            ammoText.text = "Infinity";
            return;
        }

        int currentRemain = ammo.GetCurrentRemain();
        ammoText.text = currentRemain.ToString();
        currentClipText.text = ammo.GetCurrentClip().ToString();
    }

    IEnumerator Shoot()
    {
        crossHairImage.color = Color.red;
        canShoot = false;
        if (ammo.GetCurrentClip()> 0)
        {
            w_audioSource.PlayOneShot(soundSettings.shootingSFX);
            PlayMuzzleFlash();
            ProcessRaycast();
            ammo.ReduceCurrentAmmo();
            DisplayAmmo();
            source.GenerateImpulse(Camera.main.transform.forward);
        }
        else
        {
            w_audioSource.PlayOneShot(soundSettings.emptyClipSFX);
        }
        yield return new WaitForSeconds(timeBetweenShots);
        crossHairImage.color = Color.white;
        canShoot = true;
        if (ammo.GetCurrentRemain() <= 0&&!ammo.isInfinity&&ammo.currentClip<=0)
        {
            GetComponentInParent<WeaponSwitcher>().SwitchToNextWeapon();
            AmmoReset();
            gameObject.SetActive(false);
            transform.SetParent(weaponCollection);

        }
    }

    private void PlayMuzzleFlash()
    {
        foreach(ParticleSystem particleSystem in muzzleFlashs)
        {
            particleSystem.Play();
        }
    }

    private void ProcessRaycast()
    {
        if (!isShotgun)
        {
            RaycastHit hit;
            if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
            {
                CreateHitImpact(hit);
                EnemyHealth target = hit.transform.GetComponentInParent<EnemyHealth>();
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
                EnemyHealth target = shotHit.transform.GetComponentInParent<EnemyHealth>();
                if (target == null) return;
                float distanceHitGun = Vector3.Distance(shotHit.point, FPCamera.transform.position);
                //float decayedDamage = (1 - distanceHitGun / range) * damage;
                target.TakeDamage(damage);
            }

        }
    }
    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .5f);
    }


    private void CreateHitImpact(RaycastHit[] hits)
    {


    }
    private void OnDrawGizmos()
    {
        if (isShotgun)
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
