using UnityEngine;
using Mirror;
public class StateMachine : NetworkBehaviour
{
    protected BaseState currentState;
    private void Start()
    {
        currentState = getInitialState();
        if (currentState != null)
            currentState.Enter();
    }
    private void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
    }
    private void LateUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();
    }
    public void ChangeState(BaseState newState)
    {
        if (newState == currentState)
            return;

        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
    protected virtual BaseState getInitialState()
    {
        return null;
    }
}