using System;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public BaseState CurrentState { get; private set; }

    private void Start()
    {
        CurrentState = GetInitialState();
        CurrentState?.Enter();
    }

    private void Update()
    {
        CurrentState?.UpdateLogic();
    }

    private void LateUpdate()
    {
        CurrentState?.UpdatePhysics();
    }

    public void ChangeState(BaseState newState)
    {
        if (newState == CurrentState)
        {
            Debug.LogError("Transitioned to the Same State!");
            throw new Exception();
        }

        CurrentState?.Exit();
        CurrentState = newState;
        CurrentState?.Enter();
    }
    protected abstract BaseState GetInitialState();
}