using Friends;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace UI
{
    public class GameOverScreen : MonoBehaviour
    {
        private UIDocument doc;
        private ScoreKeeper scoreKeeper;
    
        [SerializeField] private VisualTreeAsset gameOverScreen;
        private void Start()
        {
            doc = GetComponent<UIDocument>();
            scoreKeeper = ScoreKeeper.instance;
            Time.timeScale = 1;
        }

        public void Initialize()
        {
            Time.timeScale = 0;
            doc.visualTreeAsset = gameOverScreen;
        
            doc.rootVisualElement.Q<VisualElement>("exitButtonGOScreen").AddManipulator(new Clickable(() =>
            {
                Application.Quit(0);
            }));
        
            doc.rootVisualElement.Q<VisualElement>("mainMenuButtonGOScreen").AddManipulator(new Clickable(() =>
            {
                SceneManager.LoadScene(1);
                ScoreKeeper.score = 0;
            }));
            doc.rootVisualElement.Q<Label>("finalScoreText").text = ScoreKeeper.score.ToString();
        }
    }
}
