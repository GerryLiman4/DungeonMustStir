using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine, StateID previousStateID = StateID.Idle)
    {
        this.stateMachine = stateMachine;
        this.PreviousStateID = previousStateID;
    }
    public override void Enter()
    {
        //stateMachine.playerInputReader.Jump += OnJump;
        //stateMachine.playerInputReader.Move += OnMove;
        //stateMachine.playerInputReader.Stop += OnStop;
        //stateMachine.playerInputReader.Sprint += OnSprint;
        //stateMachine.playerInputReader.Crouch += OnCrouch;
        //stateMachine.playerInputReader.Attack += OnAttack;
    }

    public override void Exit()
    {
        //stateMachine.playerInputReader.Jump -= OnJump;
        //stateMachine.playerInputReader.Move -= OnMove;
        //stateMachine.playerInputReader.Stop -= OnStop;
        //stateMachine.playerInputReader.Sprint -= OnSprint;
        //stateMachine.playerInputReader.Crouch -= OnCrouch;
        //stateMachine.playerInputReader.Attack += OnAttack;
    }
    //protected abstract void Flip(Vector2 direction);

    public override void Tick(float deltaTime)
    {
        stateMachine.playerMovementController.JumpAndGravity();
        stateMachine.playerMovementController.GroundedCheck();
        stateMachine.playerMovementController.Move();
        //if (!stateMachine.groundDetector.CheckGround())
        //{
        //    stateMachine.SwitchState(new PlayerFallState(this.stateMachine, CurrentStateID));
        //}
        //stateMachine.playerMovementController.Move(stateMachine.playerInputReader.movement);
    }

    public override void LateTick()
    {

    }

    //protected abstract void OnJump();
    //protected abstract void OnMove(Vector2 direction);
    //protected abstract void OnStop();
    //protected abstract void OnSprint();
    //protected abstract void OnCrouch();
    //protected abstract void OnAttack();
}
