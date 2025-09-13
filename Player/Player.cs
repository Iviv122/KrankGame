using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float DeathDuration = 0.2f;
    [SerializeField] CameraRotation cam;
    [SerializeField] DeathScreen deathScreen;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject pause;
    public void Die()
    {
        Debug.Log("Dead");
        cam.enabled = false;
        deathScreen.Appear();
        pause.SetActive(false);
        settings.SetActive(false);
        cam.gameObject.transform.DORotate(new Vector3(-90, 0, 0), DeathDuration, RotateMode.Fast);
    }
}
