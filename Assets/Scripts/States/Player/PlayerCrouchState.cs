using UnityEngine;

public class PlayerCrouchState : PlayerBaseState
{
    public override StateID CurrentStateID { get; set; }
    public override StateID PreviousStateID { get; set; }

    public PlayerCrouchState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
    {
        this.stateMachine = stateMachine;
        this.CurrentStateID = StateID.Crouch;
        this.PreviousStateID = previousStateID;
    }

    public override void Enter()
    {
        base.Enter();
        stateMachine.characterController.height = stateMachine.playerConfiguration.hitboxCrouchHeight;
        stateMachine.characterController.center = stateMachine.playerConfiguration.hitboxCrouchOffset;
    }

    public override void Exit()
    {
        base.Exit();
        stateMachine.characterController.height = stateMachine.playerConfiguration.hitboxOriginalHeight;
        stateMachine.characterController.center = stateMachine.playerConfiguration.hitboxOriginalOffset;
    }

    public override void FixedTick()
    {

    }

    public override void LateTick()
    {
        base.LateTick();
    }

    public override void Tick(float deltaTime)
    {
        stateMachine.playerMovementController.JumpAndGravity();
        stateMachine.playerMovementController.GroundedCheck();
        stateMachine.playerMovementController.Crouch();
        stateMachine.playerMovementController.Roll();
    }
 
}
