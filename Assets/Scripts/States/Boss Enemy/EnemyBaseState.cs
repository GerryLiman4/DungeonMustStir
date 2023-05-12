using UnityEngine;

public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;

    public EnemyBaseState(EnemyStateMachine stateMachine, StateID previousStateID = StateID.Idle)
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

    public override void Tick(float deltaTime)
    {
        //stateMachine.playerMovementController.JumpAndGravity();
        //stateMachine.playerMovementController.GroundedCheck();
        //stateMachine.playerMovementController.Move();
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
