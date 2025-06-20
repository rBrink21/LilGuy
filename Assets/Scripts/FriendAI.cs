using System;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpringJoint2D))]
public class FriendAI : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private CircleCollider2D myCollider;
    private SpringJoint2D spring;
    private BulletShooter bs;

    [Header("Movement")]
    [Tooltip("How far the friend will float away from the player")]
    [SerializeField] private float springDistance;
    
    [Tooltip("Amount of velocity lost per 'spring', 0-1")]
    [SerializeField][Range(0,1)] private float springDampening;

    [Header("Targeting")]
    [Tooltip("How far the enemies have to be for this friend to shoot at it. Visualized with a blue orb.")]
    [SerializeField] private float targetingDistance = 1f;
    
    [Tooltip("We dont want to run this every frame since it is expensive so there is a small delay, adjust down if friends take too long to find a new target.")]
    [SerializeField] private float timeBetweenTargetUpdates = 0.5f;
    private float timeSinceLastTargetUpdate = Mathf.Infinity;
    
    
    
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spring = GetComponent<SpringJoint2D>();
        myCollider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        bs = GetComponent<BulletShooter>();
        spring.distance = springDistance;
        spring.dampingRatio = springDampening;
        
        if (spring.attachedRigidbody == null)
        {
            Debug.LogWarning("Player not attached to the SpringJoint2D on this friend!");
        }
        if (bs != null)
        {
            bs.friendlyShooter = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, targetingDistance);
    }

    private void Update()
    {
        timeSinceLastTargetUpdate += Time.deltaTime;
        if (bs == null) return;
        if (timeSinceLastTargetUpdate > timeBetweenTargetUpdates)
        {
            bs.SetTarget(FindTarget());
        }
    }

    private Transform FindTarget()
    {
        timeSinceLastTargetUpdate = 0;
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        var closestEnemyDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (var enemy in enemies)
        {
            var distance = Vector2.Distance(enemy.transform.position, transform.position);
            if (distance < closestEnemyDistance)
            {
                closestEnemy = enemy;
                closestEnemyDistance = distance;
            }
        }
        
        return closestEnemy != null ? closestEnemy.transform : null;
    }
}
