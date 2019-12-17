using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Camera FPSCamera;
    [SerializeField] float range=100f;
    [SerializeField] float damage=30f;
    [SerializeField] ParticleSystem Flash;
    [SerializeField] GameObject HitFX;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    private void Shoot()
    {
        ProcessRaycast();
        PlayFlashFX();
    
    }
    private void PlayFlashFX(){
        Flash.Play();
    }
    private void ProcessRaycast()
    {
        RaycastHit hit;
        if (Physics.Raycast(FPSCamera.transform.position, FPSCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
            Debug.Log("I hit:" + hit.transform.name);
        }
    }

    private void CreateHitImpact(RaycastHit hit){
        GameObject impact=Instantiate(HitFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact,.1f);
    }
}
