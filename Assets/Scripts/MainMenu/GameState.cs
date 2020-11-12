using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState:MonoBehaviour
{
    public int cameraIndex;
    public bool selected;
    public abstract void TransitionExit();
    public abstract void TransitionEnter();
}
