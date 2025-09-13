using UnityEngine;

public class CameraFov : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] Settings setttings;
    private void Awake()
    {
        setttings.Load();
        setttings.Updated += ChangeFOV;
        ChangeFOV(); 
    }
    void ChangeFOV()
    {
        cam.fieldOfView = setttings.FOV;
    }
}
