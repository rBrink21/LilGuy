using System;
using Friends;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class ScoreKeeper : MonoBehaviour
{
    public static ScoreKeeper instance;
    
    private VisualElement doc;
    public Action<int> ScoreUpdated;
    public static int score = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        doc = FindFirstObjectByType<UIDocument>().rootVisualElement;
        LoadScoreText();
        UpdateProgressBar();
        // SubscribeToSpawners();
        SceneManager.sceneLoaded += Initialize;
    }

    private void FixedUpdate()
    {
        LoadScoreText();
    }

    
    private void Initialize(Scene scene, LoadSceneMode mode)
    {
       doc = FindFirstObjectByType<UIDocument>().rootVisualElement;
       LoadScoreText();
       UpdateProgressBar();
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
        var range = FriendManager.instance.GetScoreRangeCost();

        prog.lowValue = range.low;
        prog.highValue = range.high;
        prog.value = score;
    }
    
    public void LoadScoreText()
    {
        doc = FindFirstObjectByType<UIDocument>().rootVisualElement;
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
