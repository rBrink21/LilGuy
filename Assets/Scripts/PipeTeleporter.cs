using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PipeTeleporter : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("hit");
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);

        }
    }
}
