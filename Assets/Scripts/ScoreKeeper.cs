using System;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoreKeeper : MonoBehaviour
{
    private VisualElement doc;

    private int score = 0;
    
    private void Start()
    {
        doc = GetComponent<UIDocument>().rootVisualElement;
        

        var enemySpawners = FindObjectsByType<EnemySpawner>(FindObjectsSortMode.None);
        foreach (EnemySpawner enemySpawner in enemySpawners)
        {
            enemySpawner.enemyCreated += (enemyHealth) =>
            {
                print("reached");
                enemyHealth.hasDied += UpdateScore;
            };
        }
    }

    private void UpdateScore()
    {
        print("updated!");
        var scoreValue = doc.Q<Label>("scoreValue");
        score++;
        scoreValue.text = score.ToString();
    }
    
    // 350225640362736
}
