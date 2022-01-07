using System;
using UnityEngine;

public class ShowDirectionObject : MonoBehaviour
{
    [SerializeField] private Transform _object = default;
    [SerializeField] private Direction _directionToShow = Direction.Forward;
    [SerializeField] private Color _rayColor = Color.black;
    [SerializeField] private float _raySize = 5.0f;

    void OnDrawGizmos()
    {
        Gizmos.color = _rayColor;
        Vector3 selectDirection = GetVectorDirection(_directionToShow);
        Vector3 direction = transform.TransformDirection(selectDirection) * _raySize;
        Gizmos.DrawRay(_object.position, direction);
    }

    private Vector3 GetVectorDirection(Direction direction)
    {
        switch(direction)
        {
            case Direction.Forward : return Vector3.forward;
            case Direction.Backward: return Vector3.back;
            default:  return Vector3.zero;
        }
    }

    [Serializable]
    private enum Direction
    {
        Forward = 0,
        Backward
    }
}
