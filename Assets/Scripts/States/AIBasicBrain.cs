using UnityEngine;
public class AIBasicBrain : AIBrain
{
    [Range(0,10)]
    [SerializeField] public float alarmedRange = 5f;
    [Range(0,5)]
    [SerializeField] public float raycastSphereRadius = 2f;
    [SerializeField] private GameObject enemyDetectorRoot;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float fps = 5;
    [SerializeField] private float fpsCounter;

    public override void Attack()
    {
        
    }

    public override void Chase()
    {
        
    }

    public override void CheckEnemy()
    {
        RaycastHit[] hits;
        hits = Physics.SphereCastAll(enemyDetectorRoot.transform.position,raycastSphereRadius, transform.forward, 10, playerLayerMask);

        foreach (RaycastHit hit in hits) {
            if (hit.collider.tag == "Player") {
                Debug.Log("Enemy In range Player");
                LookEnemy();
            }
        }
    }

    public override void Dead()
    {
       
    }

    public override void LookEnemy()
    {
        Debug.Log("Look enemy");
    }

    public override void Patrol()
    {
       
    }
    protected override void Start()
    {
        base.Start();
        fpsCounter = fps;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCounter--;
        if (fpsCounter > 0) return;
        fpsCounter = fps;
        CheckEnemy();
    }
    
}
