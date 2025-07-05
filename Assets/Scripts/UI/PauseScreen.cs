using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class PauseScreen : MonoBehaviour
    {
        private bool paused;
        private UIDocument doc;
        private VisualTreeAsset originalRoot;
        [SerializeField] private VisualTreeAsset pauseScreen;
        private void Start()
        {
            doc = FindFirstObjectByType<UIDocument>();
            originalRoot = doc.visualTreeAsset;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                paused = !paused;

                if (paused)
                {
                    Pause();
                }
                else
                {
                    Unpause();
                }
            }
        }

        private void Pause()
        {
            Time.timeScale = 0;
            doc.visualTreeAsset = pauseScreen;
            doc.rootVisualElement.Q<Label>("menuText").AddManipulator(new Clickable(() => SceneManager.LoadScene(1)));
            doc.rootVisualElement.Q<Label>("exitText").AddManipulator(new Clickable(() => Application.Quit(0)));
        }

        private void Unpause()
        {
            Time.timeScale = 1;
            doc.visualTreeAsset = originalRoot;
            
            FindAnyObjectByType<HealthUIUpdater>().Init();
            FindAnyObjectByType<ScoreKeeper>().LoadScoreText();
        }
    }
}