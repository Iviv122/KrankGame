using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] String SceneName;
    public void Load()
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
}
