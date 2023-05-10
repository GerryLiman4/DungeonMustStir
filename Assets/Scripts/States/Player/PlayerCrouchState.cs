//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerCrouchState : PlayerBaseState
//{
//    private readonly int CrouchHash = Animator.StringToHash("Crouch");
//    private const float CrossFadeDuration = 0.1f;
//    public override StateID CurrentStateID { get; set; }
//    public override StateID PreviousStateID { get; set; }

//    public PlayerCrouchState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//    }


//    public override void Enter()
//    {
//        base.Enter();
//        stateMachine.animator.CrossFade(CrouchHash, 0);
//        CurrentStateID = StateID.Crouch;
//        stateMachine.playerMovementController.Stop();
//    }

//    public override void Exit()
//    {
//        base.Exit();
//    }

//    public override void FixedTick()
//    {

//    }

//    public override void LateTick()
//    {
//        base.LateTick();
//    }

//    public override void Tick(float deltaTime)
//    {
//        if (!stateMachine.groundDetector.CheckGround())
//        {
//            stateMachine.SwitchState(new PlayerFallState(this.stateMachine, CurrentStateID));
//        }

//        if (!stateMachine.playerInputReader.isCrouching)
//        {
//            stateMachine.SwitchState(new PlayerIdleState(this.stateMachine, CurrentStateID));
//        }

//    }
//    protected override void Flip(Vector2 direction)
//    {
//        if (direction.x > 0 && stateMachine.transform.localScale.x > 0) return;
//        else if (direction.x < 0 && stateMachine.transform.localScale.x < 0) return;
//        else if (direction.x == 0) return;

//        stateMachine.transform.localScale = new Vector3(-stateMachine.transform.localScale.x, stateMachine.transform.localScale.y, stateMachine.transform.localScale.z);
//    }

//    protected override void OnJump()
//    {
//        stateMachine.SwitchState(new PlayerJumpState(this.stateMachine, CurrentStateID));
//    }

//    protected override void OnMove(Vector2 direction)
//    {
//        Flip(stateMachine.playerInputReader.movement);
//    }

//    protected override void OnStop()
//    {

//    }

//    protected override void OnSprint()
//    {

//    }
//    protected override void OnCrouch()
//    {
        
//    }
//    protected override void OnAttack()
//    {
//        stateMachine.SwitchState(new PlayerAttackState(this.stateMachine, CurrentStateID));
//    }
//}
