using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeginText : MonoBehaviour
{
    private float DestroyTime=5f;
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up *2 * Time.deltaTime);
    }
}
