using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathVFXController : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        Invoke("disableParticle", 3);
        GetComponent<ParticleSystem>().Play();
    }

    private void disableParticle()
    {
        gameObject.SetActive(false);
    }
}
