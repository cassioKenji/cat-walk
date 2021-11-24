using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.StateMachine;
using Gameplay.StateMachine.ActionsAndStates;
using UnityEngine;

public class StateMachineController : MonoBehaviour
{
    public StateMachine stateMachine;

    public JumpingState jumpingState;
    public GroundedState groundedState;
    
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();

        jumpingState = GetComponent<JumpingState>();
        groundedState = GetComponent<GroundedState>();
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            stateMachine.SetState(jumpingState);
        }
        
        if (Input.GetKeyDown("r"))
        {
            stateMachine.SetState(groundedState);
        }
        
        if (Input.GetKeyDown("q"))
        {
            stateMachine.DebugStateMachine();
        }
    }

    private void OnDisable()
    {
        Debug.Log("diabling state machine controller. below you can see the last state being popped.");
        stateMachine.ExitState();
        stateMachine.ClearStates();
    }
}
