using UnityEngine;
public class Jump : BaseState
{
    private playerState _sm;
    public Jump(playerState stateMachine) : base("Jump", stateMachine) { _sm = (playerState)stateMachine; }
    public override void Enter()
    {
        base.Enter();
        _sm.anim.Play("Jump");
        _sm.velocityY = Mathf.Sqrt(_sm.jumpHeight * -2 * _sm.gravity);
    }
    public override void UpdateLogic()
    {
        base.UpdateLogic();
        _sm.takeAxisInput(_sm.airInputTimer);
        _sm.airInputTimer += Time.deltaTime;

        //isCeilinged?
        if (_sm.gcC.isCeilinged)
            _sm.velocityY = 0;

        //isFalling?
        else if (_sm.cC.velocity.y < -Mathf.Epsilon)
            _sm.ChangeState(_sm.fallState);


        _sm.stateName();
    }
    public override void UpdatePhysics()
    {
        base.UpdatePhysics();
        _sm.updatePhysics();
    }
}