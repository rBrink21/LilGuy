using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PipeTeleporter : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(2);
    }
}
