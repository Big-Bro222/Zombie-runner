using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSceneState : GameState
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

        //onStateExit();
    }
}
