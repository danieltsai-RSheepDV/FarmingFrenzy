using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    const float SMALL_NUMBER = 0.1f;
    
    [SerializeField] public InputActionAsset PI;
    private InputAction moveAction;

    [SerializeField] public float speed;
    [SerializeField] public float accel;
    [SerializeField] public float decel;
    [SerializeField] public float velPower;
    
    private Rigidbody2D rb;

    private void Start()
    {
        moveAction = PI.FindAction("Move");
        PI.Enable();
        
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnDestroy()
    {
        PI.Disable();
    }

    private void Update()
    {
        SetVelocity(moveAction.ReadValue<Vector2>() * speed);
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
