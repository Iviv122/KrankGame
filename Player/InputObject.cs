using UnityEngine;

[CreateAssetMenu(fileName = "InputObject", menuName = "InputObject", order = 0)]
public class InputObject : ScriptableObject
{
    InputSystem_Actions inputs;
    public InputSystem_Actions Get()
    {
        if (inputs == null)
        {
            inputs = new();
        }
        return inputs;
    }
} 
