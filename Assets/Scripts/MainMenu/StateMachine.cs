using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UIState
{
    public GameState GeneralState;
    public GameState MapSelectionState;
    public GameState MultiPlayerState;
    public GameState WeaponSelectionState;
    public GameState GameSettingState;
}

public class StateMachine : MonoBehaviour
{
    public UIState state;
    public GameState currentState;
    public Dictionary<string, GameState> stateDic;

    private static StateMachine _instance;

    public static StateMachine Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        currentState = state.GeneralState;
        stateDic= new Dictionary<string, GameState> {
            {"GeneralState",state.GeneralState},
            {"MapSelectionState",state.MapSelectionState},
            {"MultiPlayerState",state.MultiPlayerState},
            {"WeaponSelectionState",state.WeaponSelectionState},
            {"GameSettingState",state.GameSettingState},          
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterState(string stateName)
    {
        if (stateDic.ContainsKey(stateName))
        {
            GameState State = stateDic[stateName];
            if (State == currentState) { return; }
            currentState.TransitionExit();
            State.TransitionEnter();
            currentState = State;
        }
        else
        {
            Debug.LogError("Please check the spell of State");
        }
    }
}
