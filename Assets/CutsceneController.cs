using System;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class CutsceneController : MonoBehaviour
{
    [SerializeField] private float cutsceneDuration;
    [SerializeField] private float startDelay;
    private bool started;
    private float timeElapsed;
    [SerializeField] private int nextSceneIndex = 0;

    private void Update()
    {
        timeElapsed += Time.deltaTime;

        if (!started && timeElapsed > startDelay)
        {
            FindFirstObjectByType<VideoPlayer>().Play();
        }
        
        if (timeElapsed > cutsceneDuration)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
