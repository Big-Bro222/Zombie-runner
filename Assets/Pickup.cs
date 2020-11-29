using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private float rotationSpeed=150;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,rotationSpeed * Time.deltaTime,0));
    }
}
