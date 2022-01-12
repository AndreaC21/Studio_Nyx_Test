using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Rigidbody _rigibody;
    [SerializeField] private Transform _target;

    private float _moveSpeed = 4;
    private float _rotationSpeed = 50;
    private bool _isMoving;
    private bool _isRotating;

    private float _moveDirection2;
    private Vector3 _directionRotation;

    private float _rotateDirection;

    private Vector3 directionTest = Vector3.forward * 4;

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
        //MovementSimple();
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
    /// <summary>
    /// Horizontal and Vertical movement
    /// </summary>
    private void MovementSimple()
    {
        Vector3 directionInput = new Vector3(_rotateDirection, 0, _moveDirection2 );
        _rigibody.AddForce(directionInput * 5);
    }

    private void MovementWithForce()
    {
        //Vector3 direction = _rigibody.transform.position - transform.position;
        Vector3 directionMovement = transform.forward * _moveDirection2 ;
        Vector3 directionRotation = Vector3.up * _rotateDirection ;

        Vector3 direction = GetWorldForwardDirection() * _moveDirection2;
        Debug.Log("Direction forward :" + direction);


        // direction = new Vector3(0, 0.0f, _moveDirection2) * 2;

        //  Debug.Log(d);

        if (_isRotating)
        {
            _rigibody.AddTorque( directionRotation * _rotationSpeed * Time.deltaTime);
        }
        if (_isMoving)
        {
            //  _rigibody.AddForceAtPosition(direction, transform.position);
            //direction = _target.position - transform.position;
            _rigibody.AddForce(direction * 5);
        }
      //  direction = _target.position - transform.position;
       
       

      /*  Quaternion q = Quaternion.AngleAxis(transform.localEulerAngles.y, Vector3.left);
        Vector3 targetForward = q * Vector3.forward;
        Vector3 targetUp = q  * Vector3.up;

       // Debug.Log("Direction :" + targetForward +" --------- "+targetUp);

        if (_isRotating)
        {
            _rigibody.AddTorque(targetUp * _rotateDirection * _rotationSpeed);
        }
        if (_isMoving)
        {
            //  _rigibody.AddForceAtPosition(direction, transform.position);
            Vector3 d = targetForward * _moveDirection2 * _moveSpeed;
            _rigibody.AddForce(d);
            Debug.Log("Direction :" + d);
        }*/
        
    }

    private Vector3 GetWorldForwardDirection()
    {
        float yAngle = _rigibody.rotation.eulerAngles.y;
      //  float xAngle = _rigibody.rotation.eulerAngles.x;
        Quaternion qY = Quaternion.AngleAxis(yAngle, Vector3.up);
       
        Vector3 forward = qY * Vector3.forward;
        Debug.Log("B Forward: " + forward.normalized);
        float f = GetRotationFactor(_rigibody.rotation);
        // float xCos = Mathf.Cos(xAngle * Mathf.Deg2Rad);
        forward *= f;
       // forward.z *= xCos;
        Debug.Log("A Forward: "+forward.normalized +" Factor: "+f);

        return forward.normalized;
    }

    private int GetRotationFactor(Quaternion q)
    {
        Debug.Log("Quaternion: "+q);
         if (Mathf.Abs(q.x) > Mathf.Abs(q.w))
         {
            bool a = q.y <= 0 && q.x >= 0;
            bool b = q.y > 0 && q.x < 0;
            bool c = Mathf.Abs(q.x) > Mathf.Abs(q.y);

            if (a || b || c)
            {
                 return -1;
            }
         }
        /*else if (q.x < q.w)
        {
            return 1;
        }*/
        return 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 selectDirection = GetWorldForwardDirection();
        float r = 5;
        Vector3 offset = Vector3.forward * 5;
       
        Gizmos.DrawRay(transform.position,selectDirection * r);
        Gizmos.DrawSphere(transform.position + selectDirection * r, 0.5f);
    }
}
