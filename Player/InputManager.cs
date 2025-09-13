using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] LevelRestart restart;
    [SerializeField] InputObject input;
    public event Action<Vector2> Move;
    public event Action<Vector2> Look;
    public event Action LeftMouse;
    public event Action Jump;
    public event Action Pause;

    private void Awake()
    {
        restart.Cleaning += Clean;
        input.Get();
    }
    void OnEnable()
    {
        input.Get().Player.Attack.performed += OnAttack;
        input.Get().Player.Move.performed += OnMove;
        input.Get().Player.Look.performed += OnLook;
        input.Get().Player.Restart.performed += OnRestart;
        input.Get().Player.Jump.performed += OnJump;
        input.Get().Player.Pause.performed += OnPause;

        input.Get().Player.Attack.Enable();
        input.Get().Player.Move.Enable();
        input.Get().Player.Look.Enable();
        input.Get().Player.Restart.Enable();
        input.Get().Player.Jump.Enable();
        input.Get().Player.Pause.Enable();
    }
    private void OnPause(InputAction.CallbackContext obj)
    {
        Pause?.Invoke();
    }
    private void OnLook(InputAction.CallbackContext obj)
    {
        Vector2 dir = obj.ReadValue<Vector2>();
        Look?.Invoke(dir);
    }
    private void OnMove(InputAction.CallbackContext obj)
    {
        Vector2 dir = obj.ReadValue<Vector2>();
        Move?.Invoke(dir);
    }
    private void OnJump(InputAction.CallbackContext obj)
    {
        Jump?.Invoke();
    }
    private void OnAttack(InputAction.CallbackContext obj)
    {
        LeftMouse?.Invoke();
    }
    private void Clean()
    {
        input.Get().Player.Attack.performed -= OnAttack;
        input.Get().Player.Move.performed -= OnMove;
        input.Get().Player.Look.performed -= OnLook;
        input.Get().Player.Restart.performed -= OnRestart;
        input.Get().Player.Jump.performed -= OnJump;
        input.Get().Player.Pause.performed -= OnPause;

        input.Get().Player.Attack.Disable();
        input.Get().Player.Move.Disable();
        input.Get().Player.Look.Disable();
        input.Get().Player.Restart.Disable();
        input.Get().Player.Jump.Disable();
        input.Get().Player.Pause.Disable();

        restart.Cleaning -= Clean;
    }
    private void OnRestart(InputAction.CallbackContext obj)
    {
        Clean();
        restart.Reset();
    }
}