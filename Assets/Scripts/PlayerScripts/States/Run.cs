using UnityEngine;
public class Run : BaseState
{
    private playerState _sm;
    public Run(playerState stateMachine) : base("Run", stateMachine) { _sm = (playerState)stateMachine; }
    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Run");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.takeAxisInput(1);

        if (_sm.horizontalInput == 0 && _sm.verticalInput == 0)
            _sm.ChangeState(_sm.idleState);

        else if (Input.GetKeyDown(KeyCode.Space) && _sm.gcC.isGrounded)
            _sm.ChangeState(_sm.jumpState);

        _sm.stateName();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.updatePhysics();
    }
}