using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] UIDocument ui;
    [SerializeField] String MainMenuScene;
    [SerializeField] GameObject Settings;
    [SerializeField] CameraRotation cam;
    [SerializeField] LevelRestart restart;
    void OnRestart()
    {
        Debug.Log("Restart");
        restart.Reset();
    }
    void OnExit()
    {
        Debug.Log("Exit");
        SceneManager.LoadSceneAsync(MainMenuScene);
    }
    void OnSettings()
    {
        OpenSettings();
    }
    void OpenSettings()
    {
        Settings.SetActive(true);
    }
    void OnEnable()
    {
        Stop();
    }
    private void OnDisable()
    {
        Continue();
    }
    void Stop()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        var root = ui.rootVisualElement;

        Button continueButton = root.Q<Button>("continue");
        Button restartButton = root.Q<Button>("restart");
        Button settingsButton = root.Q<Button>("settings");
        Button exitButton = root.Q<Button>("exit");

        continueButton.clicked += () =>gameObject.SetActive(false);
        restartButton.clicked += OnRestart;
        settingsButton.clicked += OnSettings;
        exitButton.clicked += OnExit;

        //continueButton.RegisterCallbackOnce<MouseUpEvent>((env) => gameObject.SetActive(false));
        //restartButton.RegisterCallbackOnce<MouseUpEvent>(OnRestart);
        //exitButton.RegisterCallbackOnce<MouseUpEvent>(OnExit);
        //settingsButton.RegisterCallbackOnce<MouseUpEvent>(OnSettings);

        cam.enabled = false;
    }
    void Continue()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        cam.enabled = true;

        Settings.SetActive(false);
    }
}
