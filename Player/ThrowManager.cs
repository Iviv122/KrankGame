using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    [SerializeField] ItemPickUp pick;
    [SerializeField] ItemThrow thrower;
    [SerializeField] InputManager manager;

    void Awake()
    {
        manager.LeftMouse += Act;
    }

    void Act()
    {
        if (thrower.HasItem)
        {
            thrower.Throw();
        }
        else
        {
            pick.PickUp();
        }
    }
}