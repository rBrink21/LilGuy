using System;
using Friends;
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
        UpdateProgressBar();
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

    public struct ScoreRange
    {
        public int low;
        public int high;
    }
    
    private void UpdateProgressBar()
    {
        var prog = doc.Q<ProgressBar>();
        var range = FindFirstObjectByType<FriendManager>().GetScoreRangeCost();

        prog.lowValue = range.low;
        prog.highValue = range.high;
        prog.value = score;
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
        UpdateProgressBar();
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
