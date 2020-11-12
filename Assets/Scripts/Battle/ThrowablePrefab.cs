using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class ThrowablePrefab : MonoBehaviour
{
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] GameObject explosionVFX;
    [SerializeField] float selfDestroyDelay=1;
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;
    [SerializeField] float explosionDemage;

    private bool isExploded=false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!isExploded)
        {
            Debug.Log("Explosion!");
            GetComponent<AudioSource>().PlayOneShot(explosionSFX);
            GetComponent<MeshRenderer>().enabled = false;
            Destroy(gameObject, selfDestroyDelay);
            isExploded = true;


            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach(Collider collider in colliders)
            {
                if (collider.GetComponent<EnemyHealth>())
                {
                    collider.GetComponent<EnemyHealth>().TakeDamage(explosionDemage);
                }
            }

        }

    }
}
