using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ScorePickup : MonoBehaviour
{
    [SerializeField] private GameObject particleEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<ScoreKeeper>().UpdateScore();
            var particleEffectInstance = Instantiate(particleEffect);
            particleEffectInstance.transform.position = transform.position;
            
            Destroy(particleEffectInstance, particleEffectInstance.GetComponent<ParticleSystem>().main.duration);
            Destroy(gameObject);
        }
    }
}
