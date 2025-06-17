using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{
    private UIDocument doc;
    private ScoreKeeper scoreKeeper;
    
    [SerializeField] private VisualTreeAsset gameOverScreen;
    private void Start()
    {
        doc = GetComponent<UIDocument>();
        scoreKeeper = FindFirstObjectByType<ScoreKeeper>();
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
            SceneManager.LoadScene(0);
        }));
        doc.rootVisualElement.Q<Label>("finalScoreText").text = scoreKeeper.score.ToString();
    }
}
