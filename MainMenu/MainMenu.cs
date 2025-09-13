using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UIDocument ui;
    [SerializeField] GameObject Settings;
    [SerializeField] String StartGame;
    [SerializeField] String TutorialScene;
    void Awake()
    {
        Stop();
    }
    void OnExit(MouseUpEvent evt)
    {
        Debug.Log("Exit");
        Application.Quit();
    }
    void OnSettings(MouseUpEvent evt)
    {
        OpenSettings();
    }
    void OpenSettings()
    {
        Settings.SetActive(true);
    }
    void Stop()
    {
        var root = ui.rootVisualElement;

        Button startButton = root.Q<Button>("start");
        Button tutorialButton = root.Q<Button>("tutorial");
        Button settingsButton = root.Q<Button>("settings");
        Button exitButton = root.Q<Button>("exit");

        startButton.RegisterCallback<MouseUpEvent>((env) => SceneManager.LoadScene(StartGame));
        tutorialButton.RegisterCallback<MouseUpEvent>((env) => SceneManager.LoadScene(TutorialScene));
        exitButton.RegisterCallback<MouseUpEvent>(OnExit);
        settingsButton.RegisterCallback<MouseUpEvent>(OnSettings);

    }
}
