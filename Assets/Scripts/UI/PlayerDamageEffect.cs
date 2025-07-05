using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PlayerDamageEffect : MonoBehaviour
    {
        [SerializeField] private GameObject damageImage;
        [SerializeField] private AudioClip hpDamageSFX;
        [SerializeField] private float displayTime = 0.1f;
        private void Start()
        {
            damageImage.SetActive(false);
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<Health>().healthUpdated += HpDamage;
        }

        private void HpDamage(int amount)
        {
            damageImage.SetActive(true);
            GetComponent<AudioSource>().clip = hpDamageSFX;
            GetComponent<AudioSource>().Play();
            Invoke(nameof(HideDamageEffect), displayTime);
        }

        void HideDamageEffect()
        {
            damageImage.SetActive(false);

        }
    }
}