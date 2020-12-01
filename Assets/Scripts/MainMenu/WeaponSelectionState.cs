using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionState : GameState
{
    public event Action onStateEnter;
    public event Action onStateExit;
    public Transform weaponSwitcher;
    public GameObject Light;
    [SerializeField] List<GameObject> disableObjects;

    public override void TransitionEnter()
    {
        selected = true;
        //onStateEnter();
        CinemachineController.Instance.SetupCamera(cameraIndex);
        weaponSwitcher.GetComponent<Outline>().enabled = true;
        GetComponentInChildren<Carousel>().isSelected = true;
        Light.SetActive(true);

        Collider[] colliders = weaponSwitcher.GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = true;
        }

        foreach (GameObject disableObject in disableObjects)
        {
            disableObject.GetComponent<Collider>().enabled = true;
        }
    }

    public override void TransitionExit()
    {
        //onStateExit();
        selected = false;
        GetComponentInChildren<CameraTransitionTrigger>().isSelected=false;
        GetComponentInChildren<CameraTransitionTrigger>().ResetOutline();
        weaponSwitcher.GetComponent<Outline>().enabled = false;
        GetComponentInChildren<Carousel>().isSelected = false;
        Light.SetActive(false);

        Collider[] colliders = weaponSwitcher.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
        {
            collider.enabled = false;
        }

        foreach (GameObject disableObject in disableObjects)
        {
            disableObject.GetComponent<Collider>().enabled = false;
        }

    }

}
