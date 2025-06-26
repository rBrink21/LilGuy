using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PipeTeleporter : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
