using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Rigidbody _rigibody;

    private float _moveSpeed = 4;
    private float _rotationSpeed = 50;
    private bool _isMoving;
    private bool _isRotating;

    private float _moveDirection2;
    private Vector3 _directionRotation;

    private float _rotateDirection;

    private Vector3 directionTest = Vector3.forward * 4;
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
        //MovementSimple();
        //MovemenntWithCharController();
        //Movement2();
       // Movement4();
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
        Vector3 directionInput = new Vector3(_rotateDirection , 0, _moveDirection2 );
        _rigibody.AddForce(directionInput * 5);
    }

    private void MovemenntWithCharController()
    {
        Vector3 directionInput = new Vector3( _moveDirection2, 0, _rotateDirection * -1);
        _rigibody.AddTorque(directionInput * _rotationSpeed * Time.deltaTime);
    }

    private void Movement2()
    {
        Vector3 directionInput = new Vector3(_rotateDirection, 0, _moveDirection2);
        _rigibody.AddForceAtPosition(directionInput * _rotationSpeed * Time.deltaTime,transform.position);
    }


    private void Movement3()
    {
        float hor = _rotateDirection;
        float ver = _moveDirection2;

        Vector3 dir = new Vector3(hor, 0, ver);
        Vector3 rot = new Vector3(ver, 0, hor);


        cheat.position = transform.position;
        cheat.rotation = Camera.main.transform.rotation;
        Vector3 lookAt = cheat.position + cheat.forward;
        lookAt.y = transform.position.y;
        cheat.transform.LookAt(lookAt);

        _rigibody.AddTorque(cheat.TransformDirection(rot) * 50 * 0.5f);
        _rigibody.AddForce(cheat.TransformDirection(dir) * 50 * 0.5f);
    }

    private void Movement4()
    {
        float delta = _rotateDirection;
        Vector3 axis = Vector3.forward;

        _rigibody.AddTorque(axis * 50 * delta);
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
       // Debug.Log("B Forward: " + forward.normalized);
        int f = UpdateDirection();//  GetRotationFactor(_rigibody.rotation);
        // float xCos = Mathf.Cos(xAngle * Mathf.Deg2Rad);
       // forward *= f ;
       // forward.z *= xCos;
       // Debug.Log("A Forward: "+forward.normalized +" Factor: "+f);

        return forward.normalized ;
    }

    private int GetRotationFactor(Quaternion q)
    {
        Debug.Log("Quaternion: "+q);

      /*  if (Mathf.Abs(q.x) > Mathf.Abs(q.w))
        {
            Debug.Log("1");

            if (Mathf.Abs(q.z) > q.y && Mathf.Abs(q.y) > Mathf.Abs(q.w) && Mathf.Abs(q.x) < Mathf.Abs(q.z))
            {
                Debug.Log("1.1");
                return -1;
            }
            if (q.y == q.z)
            {
                Debug.Log("1.2");
                return -1;
            }



        }/*
        if (Mathf.Abs(q.x) > Mathf.Abs(q.w) && Mathf.Abs(q.y) > Mathf.Abs(q.z))
        {
            Debug.Log("1.1");
            return -1;
        }
        if (Mathf.Abs(q.z) > Mathf.Abs(q.y) && Mathf.Abs(q.x) == Mathf.Abs(q.w))
        {
            Debug.Log("2");
            return -1;
        }
        if (Mathf.Abs(q.z) > Mathf.Abs(q.y) && Mathf.Abs(q.x) == Mathf.Abs(q.w))
        {
            Debug.Log("3");
            return -1;
        }
        /* else if (Mathf.Abs(q.x) > Mathf.Abs(q.w) && (Mathf.Abs(q.y) > Mathf.Abs(q.x)))
         {
             Debug.Log("1.1");
             return -1;
         }
         /*else if (Mathf.Abs(q.x) > Mathf.Abs(q.y))
         {
             Debug.Log("1.2");
             return -1;
         }*/
        /*else if (Mathf.Abs(q.x) < Mathf.Abs(q.w))
        {
            Debug.Log("1.1");
            return -1;
        }*/

        /*else if (Mathf.Abs(q.x) == Mathf.Abs(q.w))
        {
            Debug.Log("2");
            bool a = q.x > 0 && q.y < 0 && q.z < 0;
            bool a2 = q.x < 0 && q.y > 0 && q.z > 0;
            if (a || a2)
            {
                Debug.Log("21");
                return -1;
            }
           
        }*/
        /*else if (Mathf.Abs(q.y) > Mathf.Abs(q.w))
        {
            Debug.Log("2.1");
            return -1;
        }*/

        /* else if (Mathf.Abs(q.x) > Mathf.Abs(q.y) && Mathf.Abs(q.x) == Mathf.Abs(q.z))
         {
             Debug.Log("3");
             return -1;
         }

         else if (Mathf.Abs(q.y) < Mathf.Abs(q.z))
         {
             Debug.Log("4");
             bool a = q.z < 0;
             bool a2 = q.z > 0 && q.y < 0;
             bool a3 = q.z > 0 && q.y > 0;
             bool a4 = Mathf.Abs(q.x) != Mathf.Abs(q.y);

             Debug.Log("a:" + a + " b:" + a2 + " c:" + a3 + " d1:" + a4);
             if ( (a || a2 || a3) && a4)
             {
                 Debug.Log("41");
                 return -1;
             }
         }*//*
        else if (q.x == -q.z && q.y == q.w)
        {
            Debug.Log("4");
            if (Mathf.Abs(q.y) < Mathf.Abs(q.x))
            {
                Debug.Log("41");
                return -1;
            }
        }
        else if (Mathf.Abs(q.z) == Mathf.Abs(q.w) && Mathf.Abs(q.z) > Mathf.Abs(q.x))
        {
            Debug.Log("5");
            return -1;
        }
            /*  if (Mathf.Abs(q.x) > Mathf.Abs(q.w))
              {
                  bool a = q.y <= 0 && q.x >= 0;
                  bool b = q.y >= 0 && q.x < 0;

                  bool d1 = !AlmostEquals(q.y, q.z);
                  //   bool d2 = q.y < 0 && AlmostEquals(q.y, q.z);

                  bool c = Mathf.Abs(q.x) > Mathf.Abs(q.y) && d1; //;|| d2);

                  // bool e = q.x < 0;


                  // bool d = q.x > 0 && q.y < 0 && q.z < 0;

                  Debug.Log("a:" + a + " b:" + b + " c:" + c + " d1:" + d1) ;

                  if (a || b || c )
                  {
                      return -1;
                  }

              }
              else if (Mathf.Abs(q.x) < Mathf.Abs(q.w))
              {
                  bool d2 = !AlmostEquals(q.y, q.z) && Mathf.Abs(q.y) > Mathf.Abs(q.w);
                  Debug.Log("d2:" + d2);
                  if (d2)
                  {
                      return -1;
                  }
              }
              else if (Mathf.Abs(q.x) == Mathf.Abs(q.w))
              {
                  bool d = q.x > 0 && q.y < 0 && q.z < 0;
                  bool d2 = q.x < 0 && q.y > 0 && q.z > 0;
                  bool e = Mathf.Abs(q.y) < Mathf.Abs(q.z);

                  if (d || d2 || e )
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

    private bool Change(Quaternion angle)
    {
        Quaternion q = Quaternion.Euler(90, 0, 0);
     //   Debug.Log("Q: " + q + "  Angle: " + angle);
       // Debug.Log("Q: "+q.eulerAngles +"  Angle: "+angle.eulerAngles);
        float angles = 0;
        float b = Mathf.Cos((90 / 2.0f) * Mathf.Deg2Rad) * Mathf.Cos((angle.eulerAngles.y / 2.0f) * Mathf.Deg2Rad) * Mathf.Cos((angle.eulerAngles.z / 2.0f) * Mathf.Deg2Rad);
        // Debug.Log(b);
        float c = (angle.y + angle.z) / 2.0f;
        float t = Mathf.Max(angle.w, c);

        float f = (angle.eulerAngles.y % 180) + (angle.eulerAngles.z % 180);
        bool y = angle.eulerAngles.y >= 180 || angle.eulerAngles.z >= 180;
        bool y2 = (angle.y < 0 || angle.z < 0) || SameSign(angle.y, angle.z);
    /*    if ( f >= 180)
        {
            angles = 180 - angle.eulerAngles.x;
        }*/
        if (Mathf.Abs(angle.w) <= Mathf.Abs(angle.x) && !y)
        {
            angles = 180 - angle.eulerAngles.x;
        }
        else if (Mathf.Abs(angle.w) > Mathf.Abs(angle.x) && y2)
        {

            angles = 180 - angle.eulerAngles.x;
        }
        else
        {
            angles = angle.eulerAngles.x;
        }
      //  Debug.Log(angles);
        if ( 90 < angles && angles < 180)
        {
            return true;
        }
        if (-180 < angles && angles < -90)
        {
            return true;
        }
        return false;
    }


    private bool SameSign(float a, float b)
    {
        if (a > 0 && b > 0)
        {
            return true;
        }
        if ( a < 0 && b < 0)
        {
            return true;
        }
        return false;
    }
    private int UpdateDirection()
    {
       return Change(_rigibody.rotation) ? -1 : 1;
    }

    private void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
        Vector3 selectDirection = GetWorldForwardDirection() ;
         float r = 5;

         Gizmos.DrawRay(transform.position,selectDirection * r);
         Gizmos.DrawSphere(transform.position + selectDirection * r, 0.5f);

        Debug.Log(selectDirection);
    }
}
