using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSceneState : GameState
{
    public event Action onStateEnter;
    public event Action onStateExit;
    public GameObject BackButton;
    public override void TransitionEnter()
    {
        selected = true;
        //onStateEnter();
        CinemachineController.Instance.SetupCamera(cameraIndex);
        BackButton.SetActive(false);
    }

    public override void TransitionExit()
    {
        selected = false;
        BackButton.SetActive(true);

        //onStateExit();
    }
}
