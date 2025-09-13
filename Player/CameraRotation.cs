using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] GameObject Orientation;
    [SerializeField] Settings settings;
    [SerializeField] float CameraTilt;
    [SerializeField] InputManager input;
    float rotationX;
    float rotationY;
    private float tiltInput;
    private void Awake() {
        input.Move += GetTilt;   
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        settings.Load();
        settings.Updated += () => { settings.Load(); };
    }
    void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
    void GetTilt(Vector2 d) {
        tiltInput = d.x; 
    }
    void Update()
    {
        
        float x = Input.mousePositionDelta.x * settings.MouseSensX;
        float y = Input.mousePositionDelta.y * settings.MouseSensY;

        rotationY += x;
        rotationX -= y;

        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        x = Mathf.Clamp(x, -90, 90);
        transform.rotation =Quaternion.Slerp(transform.rotation,Quaternion.Euler(rotationX,rotationY,tiltInput*CameraTilt),15f*Time.deltaTime);
        Orientation.transform.rotation = transform.rotation;
    }
}
