using System;
using Unity.Mathematics;
using UnityEngine;


public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void UpdateHealth(int change)
    {
        var newHealth = currentHealth + change;
        currentHealth = Mathf.Clamp(newHealth,0,maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
    }

    private void Die()
    {
        print("you died!");
    }
}
