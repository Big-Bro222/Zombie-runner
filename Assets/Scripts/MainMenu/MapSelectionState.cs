using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionState : GameState
{
    public event Action onStateEnter;
    public event Action onStateExit;
    [SerializeField] private GameObject spotLight;
    public override void TransitionEnter()
    {
        selected = true;
        //onStateEnter();
        CinemachineController.Instance.SetupCamera(cameraIndex);
        GetComponentInChildren<Carousel>().isSelected = true;
        spotLight.SetActive(true);
    }

    public override void TransitionExit()
    {
        selected = false;
        GetComponentInChildren<CameraTransitionTrigger>().isSelected = false;
        GetComponentInChildren<CameraTransitionTrigger>().ResetOutline();
        GetComponentInChildren<Carousel>().isSelected = false;
        spotLight.SetActive(false);
        //onStateExit();
    }
}
