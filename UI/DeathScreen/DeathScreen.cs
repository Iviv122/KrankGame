using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] RawImage img;
    [SerializeField] GameObject[] turnOffObjects;
    [SerializeField] LevelRestart restart;
    private CountdownTimer timer;
    private void Awake()
    {
        timer = new(0.5f);
        timer.OnTimerStop += () =>
        {
            Reset();
        };
    }
    private void OnEnable()
    {
        img.color = new Color(1, 0, 0, 0);
    }
    void Update()
    {
        if (timer.IsRunning)
        {
            timer.Tick(Time.deltaTime);
        }
    }
    public void Appear()
    {
        Cursor.lockState = CursorLockMode.None;
        foreach (var item in turnOffObjects)
        {
            item.SetActive(false);
        }
        img.DOFade(0.75f, 0.5f);
        timer.Start();
    }
    private void OnDestroy() {
        StopAllCoroutines();
    }
    public void Reset()
    {
        restart.Reset();
    }
}
