using System;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class ScoreKeeper : MonoBehaviour
{
    private VisualElement doc;
    public Action<int> ScoreUpdated;
    [HideInInspector] public int score = 0;
    
    private void Start()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        LoadScoreText();

        // SubscribeToSpawners();
    }

    private void FixedUpdate()
    {
        LoadScoreText();
    }

    private void SubscribeToSpawners()
    {
        var enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.enemyCreated += (enemyHealth) =>
            {
                enemyHealth.hasDied += UpdateScore;
            };
        }
    }

    public void LoadScoreText()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        var scoreValue = doc.Q<Label>("scoreValue");
        scoreValue.text = score.ToString();
    }

    public void UpdateScore()
    {
        
        score++;
        LoadScoreText();
        ScoreUpdated?.Invoke(score);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            SceneManager.LoadScene(0);
        }
    }
}
