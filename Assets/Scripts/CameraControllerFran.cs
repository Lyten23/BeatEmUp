using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllerFran : MonoBehaviour
{
    #region Variables
    [Header("Referencias")]
    public Camera cam;
    [Header("Movement")]
    public Transform target;
    public float zPosition;
    [Header("Posicionamiento")]
    public Vector2 offset;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    private Bounds _cameraBounds;
    private Vector2 _cameraSize;
    private Vector3 _targetPosition;
    #endregion
    void Start()
    {
        Initialize();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        //Vector2 size = maxPosition - minPosition;
        //Vector2 center = minPosition + size / 2f;
        //Gizmos.DrawWireCube(center, size);

        Gizmos.DrawWireCube(_cameraBounds.center, _cameraBounds.size);
    }
    private void OnValidate()
    {
        Initialize();
    }
    void FixedUpdate()
    {
        UpdatePosition();
        ApplyMovement();
    }
    private void Initialize()
    {
        //Asignamos la posicion en z
        Vector3 position = transform.position;
        position.z = zPosition;
        transform.position = position;

        _cameraBounds = new Bounds();
        _cameraBounds.SetMinMax(minPosition, maxPosition);

        //Actualizamos el tamaño de la camara
        UpdateCameraSize();
    }
    private void UpdateCameraSize()
    {
        Vector2 currentMinPosition = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 currentMaxPosition = cam.ViewportToWorldPoint(Vector2.one);
        _cameraSize = currentMaxPosition - currentMinPosition;
    }
    /// <summary>
    /// Obtiene la próxima posición a la que debe moverse la cámara
    /// </summary>
    private void UpdatePosition()
    {
        Vector3 nextPosition = target.position + (Vector3) offset;
        _targetPosition = ClampPositionInBounds(nextPosition);
        _targetPosition.z = zPosition;
    }
    /// <summary>
    /// Aplica el movimiento a la cámara
    /// </summary>
    private void ApplyMovement()
    {
        transform.position = _targetPosition;
    }
    private Vector3 ClampPositionInBounds(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, _cameraBounds.min.x + _cameraSize.x / 2f, _cameraBounds.max.x - _cameraSize.x / 2f);
        position.y = Mathf.Clamp(position.y, _cameraBounds.min.y + _cameraSize.y / 2f, _cameraBounds.max.y - _cameraSize.y / 2f);

        return position;
    }
}
