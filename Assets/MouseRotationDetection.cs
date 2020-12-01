using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotationDetection : MonoBehaviour
{
    [SerializeField] Carousel carousel;
    private Quaternion originalRotation;
    void Start()
    {
        originalRotation = transform.rotation;
    }

    private void Update()
    {
        if (!carousel.isSelected)
        {
            transform.rotation = originalRotation;
        }
    }

    private void OnMouseDrag()
    {
        Vector3 rotation = Vector3.zero;
        if (Input.GetMouseButton(0))
        {
            rotation -= new Vector3(0, Input.GetAxis("Mouse X")*10, Input.GetAxis("Mouse Y") * 10);
        }

        transform.Rotate(rotation);
        


    }

}
