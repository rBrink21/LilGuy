using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreKeeper : MonoBehaviour
{
    private VisualElement doc;

    [HideInInspector] public int score = 0;
    
    private void Start()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        

        var enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.enemyCreated += (enemyHealth) =>
            {
                enemyHealth.hasDied += UpdateScore;
            };
        }
    }

    private void UpdateScore()
    {
        var scoreValue = doc.Q<Label>("scoreValue");
        score++;
        scoreValue.text = score.ToString();
    }
}
