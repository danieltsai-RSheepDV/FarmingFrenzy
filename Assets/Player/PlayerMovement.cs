using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const float SMALL_NUMBER = 0.1f;
    
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction moveAction;
    private InputAction mouseAction;

    [SerializeField] public SpriteRenderer sp;

    [SerializeField] public float speed;
    [SerializeField] public float accel;
    [SerializeField] public float decel;
    [SerializeField] public float velPower;

    private Rigidbody2D rb;
    private Camera cam;
    private Animator anim;
    [NonSerialized] public bool onPath = false;

    [NonSerialized] public bool allowMovement = true;

    private void Start()
    {
        moveAction = PI.FindAction("Move");
        useAction = PI.FindAction("Use");
        mouseAction = PI.FindAction("Mouse");
        
        PI.Enable();
        
        rb = GetComponent<Rigidbody2D>();
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    private void OnDestroy()
    {
        PI.Disable();
    }

    private void Update()
    {
        if (!allowMovement)
        {
            SetVelocity(Vector3.zero);
            return;
        }
        
        Vector3 moveDir = moveAction.ReadValue<Vector2>();
        anim.SetBool("Walking", moveDir.magnitude > SMALL_NUMBER);
        SetVelocity(moveDir * (useAction.inProgress ? 0.5f : 1f) * (onPath ? 1.3f : 1f) * speed);
        sp.flipX = cam.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>()).x > transform.position.x;
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
