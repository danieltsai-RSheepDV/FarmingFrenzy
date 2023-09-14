using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PotatoController : MonoBehaviour
{
    enum States
    {
        ROLLING,
        IDLE
    }

    private States curState = States.IDLE;
    
    const float SMALL_NUMBER = 0.1f;

    [SerializeField] public SpriteRenderer sp;
    [Space] 
    [SerializeField] private float damageAmount;
    [SerializeField] private float detectionDistance;
    [Space]
    [SerializeField] public float speed;
    [SerializeField] public float turnSpeed;
    [SerializeField] private float driftDistance = 3f;
    [SerializeField] private float pauseTime = 1f;
    [SerializeField] private float knockbackAmount;
    [Space]
    [SerializeField] public float accel;
    [SerializeField] public float decel;
    [SerializeField] public float velPower;

    private float timer;
    private bool initialized;
    
    private GameObject target;
    private Vector3 direction;
    private float usedAngle;
    private float distanceTravelled;

    private Rigidbody2D rb;
    private GameObject player;
    //TODO: Replace with house
    private GameObject house = null;
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        timer = pauseTime + Random.Range(-0.5f, 0.5f);
        player = GameManager.Player;
        house = GameObject.Find("House");
        
        target = house;

        initialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > SMALL_NUMBER)
        {
            sp.flipX = rb.velocity.x > 0;
            anim.SetBool("Reversed", rb.velocity.x > 0);
        }

        if (curState == States.ROLLING)
        {
            if (target == null)
            {
                curState = States.IDLE;
                return;
            }
            
            if (Mathf.Abs(usedAngle) < 135)
            {
                Vector3 targetDir = (target.transform.position - transform.position).normalized;
                Vector3 newDir = Vector3.RotateTowards(direction, targetDir, turnSpeed * Time.deltaTime, 0f);
                usedAngle += Vector3.SignedAngle(newDir, direction, Vector3.forward);
                direction = newDir;
                
                SetVelocity(direction * speed);
            }
            else
            {
                if (distanceTravelled < driftDistance)
                {
                    distanceTravelled += rb.velocity.magnitude * Time.fixedDeltaTime;
                }else{
                    curState = States.IDLE;
                    anim.SetBool("Rolling", false);
                    
                    timer = pauseTime + Random.Range(-0.5f, 0.5f);
                    usedAngle = 0f;
                    distanceTravelled = 0f;
                    
                    return;
                }
            }
        }
        else if (curState == States.IDLE)
        {
            SetVelocity(Vector3.zero);
            
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                return;
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
                if (Vector3.Distance(player.transform.position, transform.position) < detectionDistance + 3)
                {
                    target = house;
                }
            }
            if (target == null) return;
            
            curState = States.ROLLING;
            anim.SetBool("Rolling", true);
                
            direction = (target.transform.position - transform.position).normalized;
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

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(!initialized) return;

        Health h = col.gameObject.GetComponent<Health>();
        if (h && col.gameObject == GameManager.Player)
        {
            h.Damage(damageAmount);
        }
        
        rb.velocity = -rb.velocity.normalized * knockbackAmount;
        
        curState = States.IDLE;
        anim.SetBool("Rolling", false);
                    
        timer = pauseTime;
        usedAngle = 0f;
        distanceTravelled = 0f;
    }
}
