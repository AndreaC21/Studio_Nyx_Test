using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]

public class InputManager : MonoBehaviour
{
    private PlayerControl _playerControls;

    private void Awake()
    {
        _playerControls = new PlayerControl();
    }

    private void OnEnable()
    {
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        _playerControls.Disable();
    }

    private void Start()
    {
        _playerControls.Land.Move.started += StartMove;
        _playerControls.Land.Move.canceled += StopMove;

        _playerControls.Land.Rotate.started += StartRotate;
        _playerControls.Land.Rotate.canceled += StopRotate;

        _playerControls.Land.ReduceHealth.performed += ReduceHealth;
        _playerControls.Land.IncreaseHealth.performed += IncreaseHealth;
    }

    private void OnDestroy()
    {
        _playerControls.Land.Move.started -= StartMove;
        _playerControls.Land.Move.canceled -= StopMove;

        _playerControls.Land.Rotate.started -= StartRotate;
        _playerControls.Land.Rotate.canceled -= StopRotate;

        _playerControls.Land.ReduceHealth.performed -= ReduceHealth;
        _playerControls.Land.IncreaseHealth.performed -= IncreaseHealth;
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        float direction = context.ReadValue<float>();
        if (OnStartMove!=null)
        {
            OnStartMove(direction);
        }
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        if (OnStopMove != null)
        {
            OnStopMove();
        }
    }

    private void StartRotate(InputAction.CallbackContext context)
    {
        if (OnStartRotate != null)
        {
            float direction = context.ReadValue<float>();
            OnStartRotate(direction);
        }   
    }

    private void StopRotate(InputAction.CallbackContext context)
    {
        if (OnStopRotate != null)
        {
            OnStopRotate();
        }
    }

    private void ReduceHealth(InputAction.CallbackContext context)
    {
        if (OnReduceHealth != null)
        {
            OnReduceHealth();
        }
    }

    private void IncreaseHealth(InputAction.CallbackContext context)
    {
        if (OnIncreaseHealth != null)
        {
            OnIncreaseHealth();
        }
    }

    public delegate void StartMoveEvent(float direction);
    public event StartMoveEvent OnStartMove;
    public delegate void StopMoveEvent();
    public event StopMoveEvent OnStopMove;

    public delegate void StartRotateEvent(float direction);
    public event StartRotateEvent OnStartRotate;
    public delegate void StopRotateEvent();
    public event StopRotateEvent OnStopRotate;

    public delegate void ReduceHealthEvent();
    public event ReduceHealthEvent OnReduceHealth;
    public delegate void IncreaseHealthEvent();
    public event IncreaseHealthEvent OnIncreaseHealth;
}
