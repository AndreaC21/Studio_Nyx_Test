using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Rigidbody _rigibody;

    private float _moveSpeed = 4;
    private float _rotationSpeed = 180;
    private bool _isMoving;
    private bool _isRotating;

    private float _moveDirection2;
    private Vector3 _directionRotation;

    private void Start()
    {
        _inputManager.OnStartMove += StartMove;
        _inputManager.OnStopMove += StopMove;

        _inputManager.OnStartRotate += StartRotate;
        _inputManager.OnStopRotate += StopRotate;
    }

    private void OnDestroy()
    {
        _inputManager.OnStartMove -= StartMove;
        _inputManager.OnStopMove -= StopMove;

        _inputManager.OnStartRotate -= StartRotate;
        _inputManager.OnStopRotate -= StopRotate;
    }

    private void StartMove(float direction)
    {
        _moveDirection2 = direction;
        _isMoving = true; 
    }

    private void StopMove()
    {
        _isMoving = false;
    }

    private void StartRotate(float direction)
    {
        _directionRotation = new Vector3(0, direction * _rotationSpeed, 0);
        _isRotating = true;
    }

    private void StopRotate()
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
