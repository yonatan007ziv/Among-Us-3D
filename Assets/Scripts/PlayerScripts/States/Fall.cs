using UnityEngine;
public class Fall : BaseState
{
    private playerState _sm;
    public Fall(playerState stateMachine) : base("Fall", stateMachine) { _sm = (playerState)stateMachine; }
    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Fall");
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.takeAxisInput(_sm.airInputTimer);
        _sm.airInputTimer += Time.deltaTime;

        if (_sm.gcC.isGrounded)
            _sm.ChangeState(_sm.idleState);

        _sm.stateName();
    }
    public override void UpdatePhysics()
    {
        _sm.updatePhysics();
        base.UpdatePhysics();
    }
}