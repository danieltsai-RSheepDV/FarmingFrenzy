using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeaProjectileController : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    private Vector3 startPos;
    private Vector3 dir;
    
    public void Setup(GameObject target)
    {
        dir = (target.transform.position - transform.position).normalized;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(startPos, transform.position) > maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Health h = col.gameObject.GetComponent<Health>();
        if (h && col.gameObject.CompareTag("Player"))
        {
            h.Damage(damage);
            Destroy(gameObject);
        }
    }
}
