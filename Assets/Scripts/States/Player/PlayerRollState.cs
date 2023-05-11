using UnityEngine;

public class PlayerRollState : PlayerBaseState
{
    public override StateID CurrentStateID { get; set; }
    public override StateID PreviousStateID { get; set; }
    private Vector3 rollDirection = new Vector3();
    public PlayerRollState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
    {
        this.stateMachine = stateMachine;
        this.CurrentStateID = StateID.Roll;
        this.PreviousStateID = previousStateID;
    }

    public override void Enter()
    {
        base.Enter();
        rollDirection = stateMachine.transform.forward;
        rollDirection.y = 0;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.SetAnimation(AnimationId.Roll, false);
        stateMachine.playerInputReader.roll = false;
    }

    public override void FixedTick()
    {

    }

    public override void LateTick()
    {

    }

    public override void Tick(float deltaTime)
    {
        stateMachine.playerMovementController.JumpAndGravity();
        stateMachine.playerMovementController.GroundedCheck();

        AnimatorStateInfo animState = stateMachine.animator.GetCurrentAnimatorStateInfo(0);
        
        if (animState.IsName(AnimationId.Roll.ToString()) && animState.normalizedTime >= 0.9f)
        {
            stateMachine.SwitchState(new PlayerIdleState(stateMachine, CurrentStateID));
            return;
        }
        stateMachine.playerMovementController.RollMove(rollDirection);
    }
}
