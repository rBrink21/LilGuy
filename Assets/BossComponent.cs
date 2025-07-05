using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossComponent : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Health>().hasDied += TriggerCutScene;
    }

    private void TriggerCutScene()
    {
        SceneManager.LoadScene(5);
    }
}
