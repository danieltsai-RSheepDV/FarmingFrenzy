using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class SprayController : MonoBehaviour
{
    [SerializeField] public InputActionAsset PI;
    private InputAction useAction;
    private InputAction mouseAction;
    
    [SerializeField] public float sprayWidthAngle;
    [SerializeField] public float sprayDistance;

    private Camera cam;
    private Animator anim;
    
    private ParticleSystem ps;
    public List<ParticleCollisionEvent> collisionEvents;
    private ParticleSystem.ShapeModule psShape;
    private ParticleSystem.MainModule psMain;

    private Vector3 mouseDir;
    
    private void Start()
    {
        cam = Camera.main;
        anim = GetComponentInParent<Animator>();
        
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        psShape = ps.shape;
        psMain = ps.main;

        useAction = PI.FindAction("Use");
        mouseAction = PI.FindAction("Mouse");
        
        PI.Enable();
        
        ChangeSpray();
    }

    private void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(mouseAction.ReadValue<Vector2>());
        mousePos.z = 0;
        mouseDir = mousePos - transform.position;
        
        transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.up, mouseDir, Vector3.forward));

        anim.SetBool("Spraying", useAction.inProgress);
        
        if (useAction.inProgress && ps.isStopped)
        {
            ps.Play();
        }else if (!useAction.inProgress && ps.isPlaying) {
            ps.Stop();
        }
    }

    private void ChangeSpray()
    {
        psShape.angle = sprayWidthAngle;
        psMain.startLifetime = sprayDistance / 10f;
    }

    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = ps.GetCollisionEvents(other, collisionEvents);

        Health h = other.GetComponent<Health>();
        if (h && other.CompareTag("Enemy"))
        {
            h.Damage(0.25f);
        }
    }
}
