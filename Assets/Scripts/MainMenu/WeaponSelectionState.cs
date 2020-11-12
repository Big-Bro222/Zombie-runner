using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectionState : GameState
{
    public event Action onStateEnter;
    public event Action onStateExit;
    public Transform weaponSwitcher;
    public override void TransitionEnter()
    {
        selected = true;
        //onStateEnter();
        CinemachineController.Instance.SetupCamera(cameraIndex);
        weaponSwitcher.GetComponent<Outline>().enabled = true;
        GetComponentInChildren<Carousel>().isSelected = true;
    }

    public override void TransitionExit()
    {
        //onStateExit();
        selected = false;
        GetComponentInChildren<CameraTransitionTrigger>().isSelected=false;
        GetComponentInChildren<CameraTransitionTrigger>().ResetOutline();
        weaponSwitcher.GetComponent<Outline>().enabled = false;
        GetComponentInChildren<Carousel>().isSelected = false;


    }

}
