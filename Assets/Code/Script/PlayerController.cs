using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigibody;

    private PlayerControl _playerControls;
    private float _moveSpeed = 2;
    private float _rotationSpeed = 50;
    private bool _isMoving;
    private bool _isRotating;

    private float _moveDirection2;
    private Vector3 _directionRotation;

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
    }

    private void OnDestroy()
    {
        _playerControls.Land.Move.started -= StartMove;
    }

    private void StartMove(InputAction.CallbackContext context)
    {
        float v = context.ReadValue<float>();
        _moveDirection2 = v;
        _isMoving = true; 
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _isMoving = false;
    }

    private void StartRotate(InputAction.CallbackContext context)
    {
        float f = context.ReadValue<float>();
        _directionRotation = new Vector3(0, f * _rotationSpeed, 0);
        _isRotating = true;
    }

    private void StopRotate(InputAction.CallbackContext context)
    {
        _isRotating = false;
    }

    private void FixedUpdate()
    {
        if (_isRotating)
        {
            Quaternion deltaRotation = Quaternion.Euler(_directionRotation * Time.fixedDeltaTime);
            _rigibody.MoveRotation(_rigibody.rotation * deltaRotation);
        }
        
        if (_isMoving)
        {
            Vector3 directionMovement = transform.forward * _moveDirection2 * _moveSpeed;
            _rigibody.MovePosition(transform.position + directionMovement * Time.fixedDeltaTime);
        }
    }
}
