using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class WeaponZoom : MonoBehaviour
{
    [SerializeField] Camera fpsCamera;
    [SerializeField] RigidbodyFirstPersonController fpsController;
    [SerializeField] float zoomedOutFOV = 60f;
    [SerializeField] float zoomedInFOV = 20f;
    [SerializeField] float zoomOutSensitivity = 2f;
    [SerializeField] float zoomInSensitivity = .5f;
    [SerializeField] Transform weaponModelTransform;
    [SerializeField] Vector3 zoomInLocalPos;

    bool zoomedInToggle = false;

    private Vector3 originalPosition;

    private void Start()
    {
        originalPosition = weaponModelTransform.localPosition;
    }

    private void OnDisable()
    {
        ZoomOut();
    }

    private void Update()
    {
        if (zoomedInToggle)
        {
            weaponModelTransform.localPosition = Vector3.Lerp(weaponModelTransform.localPosition, zoomInLocalPos, 0.5f);
        }
        else
        {
            weaponModelTransform.localPosition = Vector3.Lerp(weaponModelTransform.localPosition, originalPosition, 0.5f);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (zoomedInToggle == false)
            {
                ZoomIn();
            }
            else
            {
                ZoomOut();
            }
        }
    }

    private void ZoomIn()
    {
        GetComponent<Weapon>().source.m_ImpulseDefinition.m_AmplitudeGain /= 2;
        zoomedInToggle = true;
        fpsCamera.fieldOfView = zoomedInFOV;
        fpsController.mouseLook.XSensitivity = zoomInSensitivity;
        fpsController.mouseLook.YSensitivity = zoomInSensitivity;
    }

    private void ZoomOut()
    {
        GetComponent<Weapon>().source.m_ImpulseDefinition.m_AmplitudeGain *= 2;
        zoomedInToggle = false;
        fpsCamera.fieldOfView = zoomedOutFOV;
        fpsController.mouseLook.XSensitivity = zoomOutSensitivity;
        fpsController.mouseLook.YSensitivity = zoomOutSensitivity;
    }
}
