using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    public float throwSpeed;
    [SerializeField] GameObject throwPrefab;
    [SerializeField] Transform ThrowPoint;
    [SerializeField] Rigidbody playerBodyRig;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Throw();
        }
    }

    private void Throw()
    {
        Vector3 Speed = ThrowPoint.transform.forward * throwSpeed + playerBodyRig.velocity;
        GameObject instancePrefab = Instantiate(throwPrefab,ThrowPoint.position,ThrowPoint.rotation);
        instancePrefab.GetComponent<Rigidbody>().velocity = Speed;
    }

}
