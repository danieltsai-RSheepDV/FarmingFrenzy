using System;
using UnityEngine;

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
    
    private GameObject target;
    private float timer;
    
    private Rigidbody2D rb;
    private GameObject player;
    //TODO: Replace with house
    private GameObject house = null;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        player = GameManager.Player;
        house = GameObject.Find("House");

        target = house;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            sp.flipX = target.transform.position.x > transform.position.x;
        }
        
        if (target == house)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < detectionDistance)
            {
                target = player;
            }
        }
        else if(target == player)
        {
            if (Vector3.Distance(player.transform.position, transform.position) > detectionDistance + 3)
            {
                target = house;
            }
        }
        if (target == null) return;

        if (curState == States.CHASING)
        {
            
            Vector3 targetDir = (target.transform.position - transform.position).normalized;
            SetVelocity(targetDir * speed);

            if (Vector3.Distance(target.transform.position, transform.position) < fireDistance)
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
                proj.GetComponent<PeaProjectileController>().Setup(target);

                timer = timePerShot;
            }
            
            if (Vector3.Distance(target.transform.position, transform.position) > fireDistance + 3)
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
