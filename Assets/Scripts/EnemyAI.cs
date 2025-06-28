using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform target;
    private BulletShooter[] bs;
    private bool hasBulletShooter;
    private Health health;

    [FormerlySerializedAs("movementSpeed")]
    [Header("Movement")]
    [SerializeField] private float acceleration;
    [SerializeField] private float maxMovementSpeed;
    [Tooltip("How far before the enemy will engage the player. Highlighted with a blue circle.")]
    [SerializeField] private float aggroRange;

    [Header("Friend Avoidance")] 
    [Tooltip("Toggles whether this enemy will try to maintain some range from the nearest ally. (Green)")][SerializeField]
    private bool shouldAvoidAllies = true;
    [SerializeField] private float avoidAlliesRange = 2f;
    [SerializeField] private float avoidAlliesForce = 0.4f;
    private GameObject closestAlly;

    [SerializeField] private GameObject deathPickup;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        bs = GetComponentsInChildren<BulletShooter>();
        health = GetComponent<Health>();
        hasBulletShooter = bs != null;
        if (hasBulletShooter)
        {
            foreach (var shooter in bs)
            {
                shooter.SetTarget(GameObject.FindGameObjectWithTag("Player").transform);
                shooter.friendlyShooter = false;
            }
            
        }

        if (health != null)
        {
            health.hasDied += HandleDeath;
        }
    }

    private void UpdateTarget(Transform trans)
    {
        foreach (var shooter in bs)
        {
            shooter.SetTarget(trans);
            shooter.friendlyShooter = false;
        }
    }
    
    private void FixedUpdate()
    {
        if (GetDistanceToTarget() < aggroRange)
        {
            UpdateTarget(target);
            FloatyMovement();
        }
        else
        {
            UpdateTarget(null);
        }
        ClampMaxSpeed();
    }

    private float GetDistanceToTarget()
    {
        return Vector2.Distance(transform.position, target.transform.position);
    }
    private void FloatyMovement()
    {
        Vector2 directionToPlayer = target.transform.position - transform.position ;
        rb.AddForce(directionToPlayer *  acceleration);

        if (!shouldAvoidAllies) return;
        AvoidAllies();   
    }

    private void ClampMaxSpeed()
    {
        if (rb.linearVelocity.magnitude > maxMovementSpeed)
        {
            rb.linearVelocity *= 0.9f;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
        if (shouldAvoidAllies)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, avoidAlliesRange);
        }
    }

    private void HandleDeath()
    {
        var score = Instantiate(deathPickup);
        score.transform.position = transform.position;
        Destroy(gameObject);
    }

    private void AvoidAllies()
    {
        var collidersNearby = Physics2D.OverlapCircleAll(transform.position, avoidAlliesRange);
       
        foreach (var c in collidersNearby)
        {
            if (c.gameObject.CompareTag("Enemy"))
            {
                var directionToCollider = c.transform.position - transform.position;
                rb.AddForce(-directionToCollider * avoidAlliesForce);
            }
        }
    }
}
