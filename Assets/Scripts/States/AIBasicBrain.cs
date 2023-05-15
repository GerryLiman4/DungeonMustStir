using UnityEngine;
public class AIBasicBrain : AIBrain
{
    [Range(0,10)]
    [SerializeField] public float alarmedRange = 5f;

    [Header("Enemy detecting area")]
    [Range(0,5)]
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
    [SerializeField] private float attackMinTime = 0.15f;
    [Range(0, 5)]
    [SerializeField] private float attackMaxTime = 3f;
    [Range(0, 5)]
    [SerializeField] private float aggroMinTime = 0.15f;
    [Range(0, 5)]
    [SerializeField] private float aggroMaxTime = 12f;
    [SerializeField] private float aggroMaxRange = 2f;

    [Tooltip("Required Component")]
    [field: SerializeField] public CharacterController characterController { get; private set; }
    [field: SerializeField] public BasicEnemyController enemyMovementController { get; private set; }

    public override void Attack()
    {
        Debug.Log("Attack");
    }

    public override void Chase()
    {
       if(target != null)
        {
           float distance = CheckTargetDistance();
            if(distance < chaseMaxDistance && distance > stopChaseDistance)
            {
                Vector3 direction = target.transform.position - enemyDetectorRoot.transform.position;
                direction.y = enemyDetectorRoot.transform.position.y;
                move = new Vector2(direction.x, direction.z);
                sprint = true;
                enemyMovementController.Move();
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
            SwitchBehaviourState(BehaviourStateID.NOTHING);
        }
    }

    public override void CheckEnemy()
    {
        if (target == null)
        {
            RaycastHit[] hits;
            hits = Physics.SphereCastAll(enemyDetectorRoot.transform.position, raycastSphereRadius, transform.forward, raycastSphereDistance, playerLayerMask);
            //Debug.DrawRay(enemyDetectorRoot.transform.position, transform.forward, Color.green, raycastSphereDistance, false);

            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == "Player")
                {
                    //Debug.DrawLine(Vector3.zero, hit.collider.transform.position, Color.magenta);
                    //Debug.DrawLine(Vector3.zero, enemyDetectorRoot.transform.position, Color.blue);
                    //Debug.Log("Enemy In range Player" + hit.collider.name);
                    Vector3 targetPosition = hit.collider.transform.position;
                    targetPosition.y = enemyDetectorRoot.transform.position.y;
                    float enemyAngle = Vector3.Angle(enemyDetectorRoot.transform.forward, targetPosition - enemyDetectorRoot.transform.position);
                    //Debug.Log("Enemy " + enemyAngle);
                    if (enemyAngle < detectorMaxAngle / 2)
                    {
                        target = hit.collider.gameObject;
                        SwitchBehaviourState(BehaviourStateID.ALARMED);
                        return;
                    }
                }
            }
        }
        else
        {
            float distance = CheckTargetDistance();
            if (distance > chaseMaxDistance)
            {
                target = null;
                SwitchBehaviourState(BehaviourStateID.NOTHING);
            }
        }
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
       
    }
    private void Aggro() 
    {
        if(target != null)
        {
            float distance = CheckTargetDistance();
            if (distance < aggroMaxRange)
            {
                Vector3 direction = enemyDetectorRoot.transform.position - target.transform.position;
                direction.y = enemyDetectorRoot.transform.position.y;
                float randomXOffset = Random.Range(-0.15f, 0.15f);
                float randomZOffset = Random.Range(-0.15f, 0.15f);
                move = new Vector2(direction.x + randomXOffset, 0f);
                sprint = false;
                enemyMovementController.Strafe();
                LookEnemy();
            }
            else
            {
                Vector3 direction = target.transform.position - enemyDetectorRoot.transform.position;
                direction.y = enemyDetectorRoot.transform.position.y;
                move = new Vector2(direction.x, direction.z);
                sprint = true;
                enemyMovementController.Move();
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
        aggroTimer = aggroMaxTime;
        enemyMovementController.Initialize(this,characterController);
    }
    private float CheckTargetDistance()
    {
        float distance = Vector3.Distance(enemyDetectorRoot.transform.position, target.transform.position);
        
        return distance;
    }
    void Update()
    {
        //fpsCounter--;
        thinkTimer -= Time.deltaTime;
        //if (fpsCounter > 0) return;
        //fpsCounter = fps;
        BehaviourStateID currentBehaviour = GetCurrentBehaviourStateId();
        //Debug.Log(currentBehaviour);
        switch (currentBehaviour)
        {
            case BehaviourStateID.NOTHING:
                CheckEnemy();
                break;
            case BehaviourStateID.PATROL:
                break;
            case BehaviourStateID.CHASE:
                Chase();
                break;
            case BehaviourStateID.WANDER:
                break;
            case BehaviourStateID.AGGRO:
                aggroTimer-= Time.deltaTime;
                Debug.Log(aggroTimer);
                if (aggroTimer > 0)
                {
                    Aggro();
                    return;
                }
                Attack();
                aggroTimer = aggroMaxTime;
                break;
            case BehaviourStateID.ALARMED:
                LookEnemy();
                SwitchBehaviourState(BehaviourStateID.CHASE);
                break;

        }
    }
    
}
