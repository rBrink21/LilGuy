using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private BulletShooter bs;
    private bool hasBulletShooter;
    private Health health;
    

    [SerializeField] private float movementSpeed;
    [SerializeField] private float maxMovementSpeed;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bs = GetComponent<BulletShooter>();
        health = GetComponent<Health>();
        hasBulletShooter = bs != null;
        if (hasBulletShooter)
        {
            bs.SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
            bs.friendlyShooter = false;
        }

        if (health != null)
        {
            health.hasDied += HandleDeath;
        }
    }

    private void FixedUpdate()
    {
        FloatyMovement();
        ClampMaxSpeed();
    }

    private void FloatyMovement()
    {
        Vector2 directionToPlayer = transform.position - target.transform.position;
        rb.AddForce(-directionToPlayer *  movementSpeed);
    }

    private void ClampMaxSpeed()
    {
        if (rb.linearVelocity.magnitude > maxMovementSpeed)
        {
            rb.linearVelocity *= 0.9f;
        }
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
