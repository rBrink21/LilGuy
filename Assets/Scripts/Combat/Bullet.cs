using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] public float lifetime;

    [Tooltip("Toggle whether this bullet should be destroyed by regular means, will still die on collision with a wall")]
    [SerializeField] public bool invulnerable;
    [SerializeField] public bool destroyAfterHit;
    

    private void Start()
    {
        var health = GetComponent<Health>();
        if (health != null)
        {
            health.hasDied += HandleDeath;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var otherHealth = other.transform.GetComponent<Health>();
        if (otherHealth == null)
        {
            Destroy(gameObject);
            return;
        }
        otherHealth.UpdateHealth(-damage);
        if (destroyAfterHit)
        {
            HandleDeath();
        };
    }

    private void HandleDeath()
    {
        if (invulnerable)
        {
            return;
        }
        Destroy(gameObject);
    }
}
