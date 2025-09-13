using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitDoor : MonoBehaviour
{
    [SerializeField] String SceneName;
    [SerializeField] LevelRestart restart;
    void Load()
    {
        restart.Clear();
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player") {
            Load();
        }
    }
}
