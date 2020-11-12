using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSelectionState : GameState
{
    public event Action onStateEnter;
    public event Action onStateExit;
    public override void TransitionEnter()
    {
        selected = true;
        //onStateEnter();
        CinemachineController.Instance.SetupCamera(cameraIndex);

    }

    public override void TransitionExit()
    {
        selected = false;
        GetComponentInChildren<CameraTransitionTrigger>().isSelected = false;
        GetComponentInChildren<CameraTransitionTrigger>().ResetOutline();

        //onStateExit();
    }
}
