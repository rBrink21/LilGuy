using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] public float lifetime;

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
            Destroy(gameObject);
        };
    }

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}
