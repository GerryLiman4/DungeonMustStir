using System.Collections.Generic;
using UnityEngine;
public class AIBasicBrain : AIBrain
{
    [Header("Enemy detecting area")]
    [Range(0, 5)]
    [SerializeField] public float raycastSphereRadius = 2f;
    [SerializeField] public float raycastSphereDistance = 3f;
    [SerializeField] private GameObject enemyDetectorRoot;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float detectorMaxAngle = 90;

    [Header("FPS")]
    [SerializeField] private float fps = 5;
    [SerializeField] private float fpsCounter;

    [Header("Chase Behaviour")]
    [SerializeField] private float chaseMaxDistance = 5f;
    [SerializeField] private float stopChaseDistance = 1f;

    [Header("Aggro Behaviour")]
    [SerializeField] private float aggroTimer = 12f;
    [Range(0, 5)]
    [SerializeField] private float aggroTime = 12f;
    [SerializeField] private float aggroMaxRange = 4f;
    [SerializeField] private float aggroToChaseRangeOffset = 2f;

    [Header("Attack Behaviour")]
    [SerializeField] private float attackTimer;
    [Range(0, 5)]
    [SerializeField] private float attackMinTime = 1.15f;

    [Header("Patrol Behaviour")]
    [SerializeField] Vector3 spawnPosition;
    [SerializeField] List<GameObject> patrolDots = new List<GameObject>();
    [SerializeField] GameObject currentPatrolDot;
    [SerializeField] private float patrolTimer;
    [Range(0, 5)]
    [SerializeField] private float patrolTime = 2.15f;

    [Header("Alarmed Behaviour")]
    [Range(0, 10)]
    [SerializeField] public float alarmedRange = 5f;

    [Tooltip("Required Component")]
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public BasicEnemyController enemyMovementController { get; private set; }
    [field: SerializeField] public Animator animator { get; private set; }

    private AnimationState currentAnimation;

    // animation IDs
    private int _animIDSpeed;
    private int _animIDGrounded;
    private int _animIDJump;
    private int _animIDFreeFall;
    private int _animIDMotionSpeed;
    private int _animIDCrouch;
    private int _animIDRoll;

    public override void Attack()
    {
        attack = true;
        Debug.Log("Attack");
        attack = false;
    }

    public override void Chase()
    {
        if (target != null)
        {
            float distance = CheckTargetDistance();
            if (distance < chaseMaxDistance && distance > stopChaseDistance)
            {
                Vector3 direction = target.transform.position - enemyDetectorRoot.transform.position;
                direction.y = enemyDetectorRoot.transform.position.y;
                move = new Vector2(direction.x, direction.z);
                sprint = true;
                enemyMovementController.Move();
            }
            else if (distance > chaseMaxDistance)
            {
                target = null;
                SwitchBehaviourState(BehaviourStateID.PATROL);
            }
            else
            {
                move = Vector2.zero;
                sprint = false;
                SwitchBehaviourState(BehaviourStateID.AGGRO);
            }
        }
        else
        {
            SwitchBehaviourState(BehaviourStateID.PATROL);
        }
    }

    public override void CheckEnemy()
    {
        if (target == null)
        {
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(enemyDetectorRoot.transform.position, raycastSphereRadius, transform.forward, raycastSphereDistance, playerLayerMask);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == "Player")
                {
                    Vector3 targetPosition = hit.collider.transform.position;
                    targetPosition.y = enemyDetectorRoot.transform.position.y;
                    float enemyAngle = Vector3.Angle(enemyDetectorRoot.transform.forward, targetPosition - enemyDetectorRoot.transform.position);

                    if (enemyAngle < detectorMaxAngle / 2)
                    {
                        target = hit.collider.gameObject;
                        SwitchBehaviourState(BehaviourStateID.ALARMED);
                        return;
                    }
                }
            }
        }
        //else
        //{
        //    float distance = CheckTargetDistance();
        //    if (distance > chaseMaxDistance)
        //    {
        //        target = null;
        //        SwitchBehaviourState(BehaviourStateID.PATROL);
        //    }
        //}
    }

    public override void Dead()
    {

    }

    public override void LookEnemy()
    {
        if (target != null)
        {
            float distance = CheckTargetDistance();
            if (distance > chaseMaxDistance)
            {
                target = null;
                SwitchBehaviourState(BehaviourStateID.NOTHING);
            }
            else
            {
                transform.LookAt(target.transform.position);
            }

        }
    }

    public override void Patrol()
    {
        Vector3 direction = new Vector3(0, 0);
        if (patrolDots.Count <= 0)
        {
            if (transform.position == spawnPosition)
            {
                move = direction;
                return;
            }
            direction = spawnPosition - transform.position;
        }
        else
        {
            if (currentPatrolDot == null)
            {
                currentPatrolDot = patrolDots[0];
            }
            else if (Vector3.Distance(transform.position, currentPatrolDot.transform.position) <= 0.1f)
            {
                patrolTimer -= Time.deltaTime;
                if (patrolTimer <= 0)
                {
                    for (int i = 0; i < patrolDots.Count; i++)
                    {
                        if (patrolDots[i] == currentPatrolDot)
                        {
                            if (i == patrolDots.Count - 1)
                            {
                                currentPatrolDot = patrolDots[0];
                            }
                            else
                            {
                                currentPatrolDot = patrolDots[i + 1];
                            }
                            break;
                        }

                    }
                    patrolTimer = patrolTime;
                }

            }

            direction = currentPatrolDot.transform.position - transform.position;
        }
        move.x = direction.x;
        move.y = direction.z;
        sprint = false;
        enemyMovementController.Move();
    }
    private void Aggro()
    {
        if (target != null)
        {
            //attack if can, if not go aggro 
            //if (aggroTimer <= 0)
            //{
            //    SwitchBehaviourState(BehaviourStateID.ATTACK);
            //    return;
            //}

            float distance = CheckTargetDistance();
            Vector3 direction = enemyDetectorRoot.transform.position - target.transform.position;
            if (distance < aggroMaxRange)
            {
                move.y = direction.z;
                sprint = false;
                enemyMovementController.Strafe();
                LookEnemy();
            }
            else
            {
                if (distance > aggroMaxRange + aggroToChaseRangeOffset)
                {
                    SwitchBehaviourState(BehaviourStateID.CHASE);
                }
                else
                {
                    move = new Vector2(0, 0);
                    sprint = false;
                    enemyMovementController.Strafe();
                    LookEnemy();
                }
            }

        }
        else
        {
            move = Vector2.zero;
            sprint = false;
            SwitchBehaviourState(BehaviourStateID.NOTHING);
        }
    }
    protected override void Start()
    {
        base.Start();
        aggroTimer = aggroTime;
        attackTimer = attackMinTime;
        patrolTime = patrolTimer;
        spawnPosition = transform.position;
        
        enemyMovementController.Initialize(this, characterController);
        AssignAnimationIDs();
        SubscribeAnimationId();
    }
    private float CheckTargetDistance()
    {
        float distance = Vector3.Distance(enemyDetectorRoot.transform.position, target.transform.position);

        return distance;
    }
    void Update()
    {
        thinkTimer -= Time.deltaTime;

        BehaviourStateID currentBehaviour = GetCurrentBehaviourStateId();
        //Debug.Log(currentBehaviour);
        switch (currentBehaviour)
        {
            case BehaviourStateID.NOTHING:
                CheckEnemy();
                break;
            case BehaviourStateID.PATROL:
                CheckEnemy();
                Patrol();
                break;
            case BehaviourStateID.CHASE:
                Chase();
                break;
            case BehaviourStateID.WANDER:
                break;
            case BehaviourStateID.AGGRO:
                aggroTimer -= Time.deltaTime;
                Debug.Log(aggroTimer);
                if (aggroTimer > 0)
                {
                    Aggro();
                    return;
                }
                int randomizeX = Random.Range(-1, 2);
                move = new Vector2(randomizeX, 0);
                aggroTimer = aggroTime;
                break;
            case BehaviourStateID.ALARMED:
                LookEnemy();
                SwitchBehaviourState(BehaviourStateID.CHASE);
                break;
            case BehaviourStateID.ATTACK:
                Attack();
                break;

        }
    }
    private void OnDestroy()
    {
        UnsubscribeAnimationId();
    }
    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash(AnimationId.Speed.ToString());
        _animIDGrounded = Animator.StringToHash(AnimationId.Grounded.ToString());
        _animIDJump = Animator.StringToHash(AnimationId.Jump.ToString());
        _animIDFreeFall = Animator.StringToHash(AnimationId.FreeFall.ToString());
        _animIDMotionSpeed = Animator.StringToHash(AnimationId.MotionSpeed.ToString());
        _animIDCrouch = Animator.StringToHash(AnimationId.Crouch.ToString());
        _animIDRoll = Animator.StringToHash(AnimationId.Roll.ToString());
    }
    private void SubscribeAnimationId()
    {
        enemyMovementController.SetAnimationBool += SetAnimation;
        enemyMovementController.SetAnimationFloat += SetAnimation;
        enemyMovementController.SetAnimationInt += SetAnimation;
        enemyMovementController.SetAnimationString += SetAnimation;
        enemyMovementController.SetAnimationTrigger += SetAnimation;
    }
    private void UnsubscribeAnimationId()
    {
        enemyMovementController.SetAnimationBool -= SetAnimation;
        enemyMovementController.SetAnimationFloat -= SetAnimation;
        enemyMovementController.SetAnimationInt -= SetAnimation;
        enemyMovementController.SetAnimationString -= SetAnimation;
        enemyMovementController.SetAnimationTrigger -= SetAnimation;
    }

    public void SetAnimation(AnimationId animationId, bool value)
    {
        // update animator if using character
        if (animator != null)
        {
            switch (animationId)
            {
                case AnimationId.Grounded:
                    animator.SetBool(_animIDGrounded, value);
                    break;
                case AnimationId.Jump:
                    animator.SetBool(_animIDJump, value);
                    break;
                case AnimationId.FreeFall:
                    animator.SetBool(_animIDFreeFall, value);
                    break;
                case AnimationId.Crouch:
                    animator.SetBool(_animIDCrouch, value);
                    break;
                case AnimationId.Roll:
                    animator.SetBool(_animIDRoll, value);
                    break;
            }
        }
    }
    public void SetAnimation(AnimationId animationId, float value)
    {
        // update animator if using character
        if (animator != null)
        {
            switch (animationId)
            {
                case AnimationId.Speed:
                    animator.SetFloat(_animIDSpeed, value);
                    break;
                case AnimationId.MotionSpeed:
                    animator.SetFloat(_animIDMotionSpeed, value);
                    break;
            }
        }
    }
    public void SetAnimation(AnimationId animationId, int value)
    {

    }
    public void SetAnimation(AnimationId animationId, string value)
    {

    }
    public void SetAnimation(AnimationId animationId)
    {

    }

}
