//using UnityEngine;

//public class PlayerAttackState : PlayerBaseState
//{
//    private readonly int AttackHash = Animator.StringToHash("Attack");
//    private const float CrossFadeDuration = 0.1f;
//    public override StateID CurrentStateID { get; set; }
//    public override StateID PreviousStateID { get; set; }

//    private AttackConfiguration previousAttack;
//    private AttackConfiguration currentAttack;
//    private AttackConfiguration nextAttack;
//    private bool lockMovement = true;
//    public PlayerAttackState(PlayerStateMachine stateMachine, StateID previousStateID) : base(stateMachine, previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//        previousAttack = null;
//    }
//    public PlayerAttackState(PlayerStateMachine stateMachine, StateID previousStateID, AttackConfiguration previousAttack) : base(stateMachine, previousStateID)
//    {
//        this.stateMachine = stateMachine;
//        this.PreviousStateID = previousStateID;
//        this.previousAttack = previousAttack;
//    }

//    public override void FixedTick()
//    {
        
//    }

//    protected override void Flip(Vector2 direction)
//    {
       
//    }

//    protected override void OnCrouch()
//    {
        
//    }

//    protected override void OnJump()
//    {
//        if (lockMovement) return;
//        stateMachine.SwitchState(new PlayerJumpState(this.stateMachine, CurrentStateID));
//    }

//    protected override void OnMove(Vector2 direction)
//    {
//        if (lockMovement) return;
//        stateMachine.SwitchState(new PlayerWalkingState(this.stateMachine, CurrentStateID));

//    }

//    protected override void OnSprint()
//    {
       
//    }

//    protected override void OnStop()
//    {
      
//    }

//    // Start is called before the first frame update
//    public override void Enter()
//    {
//        base.Enter();
//        Debug.Log("masuk" + PreviousStateID);
//        currentAttack = previousAttack == null ? stateMachine.attackController.GetFirstAttack() : stateMachine.attackController.GetNextAttack(currentAttack.attackId);
//        Debug.Log("masuk" + currentAttack);
//        Debug.Log("masuk 2" + stateMachine.attackController.GetFirstAttack().attackId);
//        if (currentAttack != null)
//        {
//            int attackHash = currentAttack == null ? AttackHash : Animator.StringToHash(currentAttack.animationName);
//            stateMachine.animator.CrossFade(attackHash, 0);
//            CurrentStateID = StateID.Attack;
//            nextAttack = stateMachine.attackController.GetNextAttack(currentAttack.attackId);
//            stateMachine.playerMovementController.Stop();
//        }
//        else
//        {
//            CurrentStateID = StateID.Attack;
//            Debug.Log("masuk idle" + currentAttack);
//            stateMachine.SwitchState(new PlayerIdleState(this.stateMachine, CurrentStateID));
//        }

//    }

//    public override void Tick(float deltaTime)
//    {
//        if (!stateMachine.groundDetector.CheckGround())
//        {
//            stateMachine.SwitchState(new PlayerFallState(this.stateMachine, CurrentStateID));
//        }
//        if (stateMachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
//        {
//            Debug.Log("masuk idle" + currentAttack);
//            stateMachine.SwitchState(new PlayerIdleState(this.stateMachine, CurrentStateID));
//        }
//        if (lockMovement == true && stateMachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f)
//        {
//            lockMovement = false;
//        }
//    }
//    public override void Exit()
//    {
//        base.Exit();
//    }
//    protected override void OnAttack()
//    {
//        return;
//        if (nextAttack != null)
//        {
//            Debug.Log("masuk idle" + currentAttack);
//            stateMachine.SwitchState(new PlayerAttackState(this.stateMachine, CurrentStateID, currentAttack));
//        }
//    }
//}
