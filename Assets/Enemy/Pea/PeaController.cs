using System;
using UnityEngine;

[RequireComponent(typeof(PathTarget))]
public class PeaController : EnemyController
{
    enum States
    {
        CHASING,
        SHOOTING
    }

    private States curState = States.CHASING;
    
    const float SMALL_NUMBER = 0.1f;
    
    [SerializeField] public SpriteRenderer sp;
    [SerializeField] public GameObject peaProjectile;
    [Space] 
    [SerializeField] private float detectionDistance;
    [SerializeField] private float fireDistance;
    [SerializeField] private float timePerShot;
    [Space]
    [SerializeField] public float speed;
    [SerializeField] public float accel;
    [SerializeField] public float decel;
    [SerializeField] public float velPower;
    
    [SerializeField] private PathTarget pathTarget;
    private float timer;
    private bool initialized;
    
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject house;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public override void Initialize()
    {
        player = GameManager.Player;
        house = GameManager.House;
        
        pathTarget.mTarget = house;

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(pathTarget.mTarget);
        if (!initialized) return;
        
        if(pathTarget.mTarget != null){
            sp.flipX = pathTarget.transform.position.x > transform.position.x;
        }
        
        if (pathTarget.mTarget == house)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < detectionDistance && player.GetComponent<Health>().GetHealth() > 0)
            {
                pathTarget.mTarget = player;
            }
        }
        else if(pathTarget.mTarget == player)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > detectionDistance + 3 || player.GetComponent<Health>().GetHealth() <= 0)
            {
                pathTarget.mTarget = house;
            }
        }

        if (pathTarget.mTarget == null)
        {
            pathTarget.mTarget = house;
            return;
        }

        if (curState == States.CHASING)
        {
            Vector3 targetDir = (pathTarget.GetCurrentWayPoint() - transform.position).normalized;
            SetVelocity(targetDir * speed);
            
            if (Vector3.Distance(pathTarget.mTarget.transform.position, transform.position) < fireDistance)
            {
                curState = States.SHOOTING;
            }
        }else if (curState == States.SHOOTING)
        {
            SetVelocity(Vector2.zero);
            
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                GameObject proj = Instantiate(peaProjectile);
                proj.transform.position = transform.position;
                proj.GetComponent<PeaProjectileController>().Setup(pathTarget.mTarget);

                timer = timePerShot;
            }
            
            if (Vector3.Distance(pathTarget.mTarget.transform.position, transform.position) > fireDistance + 3)
            {
                curState = States.CHASING;
            }
        }
    }

    private void SetVelocity(Vector2 velocity)
    {
        Vector2 curVel = rb.velocity;
        Vector2 force = Vector2.zero;
    
        // X
        float xDif = velocity.x - curVel.x;
        float xAccel = (Math.Abs(velocity.x) > SMALL_NUMBER) ? accel : decel;
        force.x = Mathf.Pow(Mathf.Abs(xDif) * xAccel, velPower) * Mathf.Sign(xDif);
    
        // Y
        float yDif = velocity.y - curVel.y;
        float yAccel = (Math.Abs(velocity.y) > SMALL_NUMBER) ? accel : decel;
        force.y = Mathf.Pow(Mathf.Abs(yDif) * yAccel, velPower) * Mathf.Sign(yDif);
    
        rb.AddForce(force);
    }
}
