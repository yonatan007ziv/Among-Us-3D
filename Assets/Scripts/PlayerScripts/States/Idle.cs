using UnityEngine;
public class Idle : BaseState
{
    private playerState _sm;
    public Idle(playerState stateMachine) : base("Idle", stateMachine) { _sm = (playerState)stateMachine; }
    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Idle");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.takeAxisInput(1);

        //isRunning?
        if (_sm.horizontalInput != 0 || _sm.verticalInput != 0)
            _sm.ChangeState(_sm.runState);

        if (Input.GetKeyDown(KeyCode.Space) && _sm.gcC.isGrounded)
            _sm.ChangeState(_sm.jumpState);

        _sm.stateName();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.updatePhysics();
    }
    public override void Exit()
    {
        base.Exit();
        _sm.airMovementVector = _sm.movementVector;
    }
}