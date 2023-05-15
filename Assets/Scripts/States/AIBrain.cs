using System;
using UnityEngine;

public abstract class AIBrain : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    //public Vector2 look;
    public bool jump;
    public bool sprint;
    public bool crouch;
    public bool roll;
    public bool attack;

    [Header("Character behavioural status")]
    protected bool nothingState;
    protected bool patrolState;
    protected bool chaseState;
    protected bool aggroState;
    protected bool alarmedState;
    protected bool wanderState;

    [SerializeField] public GameObject target;

    [SerializeField]protected BehaviourStateID currentBehaviourStateId;
    protected BehaviourStateID previousBehaviourStateId;
    public event Action<BehaviourStateID> BehaviourSwitched;

    public float actionTime = 0.5f;
    [Tooltip("normalized time of action time when ai took same state attack -> attack ")]
    public float actionTimeTolerance = 1f;
    public float thinkTimer;

    protected virtual void Start()
    {
        thinkTimer = actionTime;
    }

    public abstract void Attack();
    public abstract void Chase();
    public abstract void Patrol();
    public abstract void CheckEnemy();
    public abstract void LookEnemy();
    public abstract void Dead();


    protected void SwitchBehaviourState(BehaviourStateID nextBehaviourStateId)
    {
        if (thinkTimer > 0) return;

        if (nextBehaviourStateId == currentBehaviourStateId)
        {
            thinkTimer = actionTime * actionTimeTolerance;
            return;
        }

        previousBehaviourStateId = currentBehaviourStateId;
        currentBehaviourStateId = nextBehaviourStateId;
        thinkTimer = actionTime;
    }

    public BehaviourStateID GetCurrentBehaviourStateId() {
        return currentBehaviourStateId;
    }
}
