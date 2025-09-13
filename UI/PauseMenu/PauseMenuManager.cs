using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] InputManager input;
    void Start()
    {
        input.Pause += () =>
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
        };
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
    }
}
