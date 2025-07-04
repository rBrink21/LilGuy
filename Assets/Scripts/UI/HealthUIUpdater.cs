using UnityEngine;
using UnityEngine.UIElements;

namespace UI
{
    public class HealthUIUpdater : MonoBehaviour
    {
        private GameObject player;
        private UIDocument doc;
    
        private void Start()
        {
            Init();
        }

        public void Init()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            doc = GetComponent<UIDocument>();

            var label = doc.rootVisualElement.Q<Label>("healthValue");

            var healthComponent = player.GetComponent<Health>();

            label.text = healthComponent.currentHealth.ToString();
            
            healthComponent.healthUpdated += (newHealth) =>
            {
                label.text = newHealth.ToString();
            };

            healthComponent.hasDied += () =>
            {
                FindFirstObjectByType<GameOverScreen>().Initialize();
            };
        }
    }
}
