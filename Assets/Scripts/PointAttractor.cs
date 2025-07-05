using System;
using System.Linq;
using UnityEngine;

public class PointAttractor : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float force;

    [Tooltip("As opposed to repel")] [SerializeField]
    private bool attract;

    private void FixedUpdate()
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var c in colliders)
        {
            var rb = c.GetComponent<Rigidbody2D>();
            if (rb == null) continue;

            var direction = c.transform.position - transform.position;
            var forceDirection = attract ?  -direction.normalized : direction.normalized;
            rb.AddForce(forceDirection * force);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
