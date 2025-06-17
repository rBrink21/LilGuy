using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerComponent : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 40f;
    [SerializeField] private float maxMovementSpeed = 1f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector2(
            Input.GetAxis("Horizontal") * movementSpeed,
            Input.GetAxis("Vertical") * movementSpeed));

        if (rb.linearVelocity.magnitude > maxMovementSpeed)
        {
            rb.linearVelocity *= 0.9f;
        }
    }
}
