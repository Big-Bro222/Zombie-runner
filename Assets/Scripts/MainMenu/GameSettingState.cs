using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingState : GameState
{
    public event Action onStateEnter;
    public event Action onStateExit;
    public override void TransitionEnter()
    {
        selected = true;
        //onStateEnter();
        CinemachineController.Instance.SetupCamera(cameraIndex);
        GetComponentInChildren<ProcedualAnimation>().enabled = true;

    }

    public override void TransitionExit()
    {
        selected = false;
        GetComponentInChildren<CameraTransitionTrigger>().isSelected = false;
        GetComponentInChildren<CameraTransitionTrigger>().ResetOutline();
        GetComponentInChildren<ProcedualAnimation>().enabled = false;

        //onStateExit();
    }
}
