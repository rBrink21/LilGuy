using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class MainMenu : MonoBehaviour
{
    const int GAME_SCENE_INDEX = 2;
    
    private void Start()
    {
        var doc = GetComponent<UIDocument>();
        var startButton = doc.rootVisualElement.Q<Label>("startButton");
        startButton.AddManipulator(new Clickable(() =>
        {
            SceneManager.LoadScene(GAME_SCENE_INDEX);
        }));
        var exitButton = doc.rootVisualElement.Q<Label>("exitbutton");
        exitButton.AddManipulator(new Clickable(() =>
        {
            Application.Quit(0);
        }));
    }
}
