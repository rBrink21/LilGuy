using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    const int GAME_SCENE_INDEX = 1;
    
    private void Start()
    {
        var doc = GetComponent<UIDocument>();
        var startButton = doc.rootVisualElement.Q<VisualElement>("startButton");
        startButton.AddManipulator(new Clickable(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_INDEX);
        }));
        var exitButton = doc.rootVisualElement.Q<VisualElement>("exitbutton");
        exitButton.AddManipulator(new Clickable(() =>
        {
            Application.Quit(0);
        }));
    }
}
