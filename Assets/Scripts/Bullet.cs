using System;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float movementSpeed;
    [SerializeField] public float lifetime;

    [SerializeField] public bool destroyAfterHit;

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
}
