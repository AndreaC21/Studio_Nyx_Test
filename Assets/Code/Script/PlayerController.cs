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
    }

    private void StopMove()
    {
        _moveDirection2 = 0.0f;
    }

    private void StartRotate(float direction)
    {
        _directionRotation = Vector3.up * direction * _rotationSpeed ;// new Vector3(0, direction * _rotationSpeed, 0);
    }

    private void StopRotate()
    {
        _directionRotation = Vector3.zero;
    }


    private void FixedUpdate()
    {
        // Movement();
         MovementWithForce();
    }

    private void Movement()
    {
        if (_directionRotation != Vector3.zero)
        {
            Quaternion deltaRotation = Quaternion.Euler(_directionRotation * Time.fixedDeltaTime);
            _rigibody.MoveRotation(_rigibody.rotation * deltaRotation);
        }

        if (_moveDirection2 !=0)
        {
            Vector3 directionMovement = transform.forward * _moveDirection2 * _moveSpeed;
            _rigibody.MovePosition(transform.position + directionMovement * Time.fixedDeltaTime);
        }
    }

    private void MovementWithForce()
    {
        Vector3 direction = GetWorldForwardDirection();

        direction += _directionRotation;
        _rigibody.AddTorque(_directionRotation);
        _rigibody.AddForce(direction * _moveDirection2 * _moveSpeed);

        _directionIndicator.localEulerAngles = Vector3.up * transform.localEulerAngles.y;
        _directionIndicator.position = transform.position + GetWorldForwardDirection() * 2;
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
