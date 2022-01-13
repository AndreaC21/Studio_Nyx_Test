using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private Transform _directionIndicator;

    private float _moveSpeed = 10;
    private float _rotationSpeed = 2;
    private bool _isMoving;
    private bool _isRotating;

    private float _moveDirection2;
    private Vector3 _directionRotation;

    private float _rotateDirection;

    Transform cheat;
    private void Start()
    {
        _inputManager.OnStartMove += StartMove;
        _inputManager.OnStopMove += StopMove;

        _inputManager.OnStartRotate += StartRotate;
        _inputManager.OnStopRotate += StopRotate;
        cheat = new GameObject().transform;
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
        _moveDirection2 = 0.0f;
        _isMoving = false;
    }

    private void StartRotate(float direction)
    {
        _directionRotation = new Vector3(0, direction * _rotationSpeed, 0);
        _rotateDirection = direction;
        _isRotating = true;
    }

    private void StopRotate()
    {
        _directionRotation = Vector3.zero;
        _rotateDirection = 0.0f;
        _isRotating = false;
    }

    private void FixedUpdate()
    {
        // Movement();
         MovementWithForce();
    }

    private void Movement()
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

    private void MovementWithForce()
    {
        
        if (_isMoving && _isRotating)
        {
            return;
        }

        Vector3 directionRotation = Vector3.up * _rotateDirection ;
        if (_isRotating)
        {
            _rigibody.AddTorque(directionRotation * _rotationSpeed);
        }

        Vector3 direction = GetWorldForwardDirection() * _moveDirection2;
        if (_isMoving)
        {
            _rigibody.AddForce(direction * _moveSpeed);
        }
        _directionIndicator.position = transform.position + GetWorldForwardDirection() * 2;
        _directionIndicator.localEulerAngles = Vector3.up * transform.localEulerAngles.y;

    }

    private Vector3 GetWorldForwardDirection()
    {
        Vector3 forward = Vector3.Cross(transform.right, Vector3.up);
        return forward.normalized ;
    }


    private void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
         Vector3 selectDirection =  GetWorldForwardDirection() ;
         float radius = 5;

         Gizmos.DrawRay(transform.position,selectDirection * radius);
         Gizmos.DrawSphere(transform.position + selectDirection * radius, 0.5f);
    }
}
