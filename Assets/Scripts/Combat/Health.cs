using System;
using Unity.Mathematics;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    public int currentHealth;
    public Action<int> healthUpdated;
    public Action hasDied;
    
    private void Start()
    {
        currentHealth = maxHealth;
        healthUpdated?.Invoke(currentHealth);
    }

    public void UpdateHealth(int change)
    {
        var newHealth = currentHealth + change;
        currentHealth = Mathf.Clamp(newHealth,0,maxHealth);
        if (currentHealth == 0)
        {
            Die();
        }
        
        healthUpdated?.Invoke(currentHealth);


        var flashEffect = GetComponent<FlashEffect>();
        if (flashEffect != null)
        {
            flashEffect.Activate();
        }
    }

    private void Die()
    {
        hasDied?.Invoke();
    }
}
