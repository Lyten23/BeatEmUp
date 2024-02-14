using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [Header("Referencias")] 
    public Camera cam;
    [Header("Movement")] 
    public Transform target;
    public float zPosition;
    [Header("Posicionamiento")] 
    public Vector2 offset;
    [SerializeField]private Vector2 _cameraSize;
    private Vector3 _targetPosition;
    public Transform objectTransform;
    public float angleX;
    public float angleY;
    #endregion
    void Start()
    {
        Initialize();
    }
    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        UpdatePosition();
        ApplyMovement();
    }

    void Initialize()
    {
        // Asignamos la posiciçon en z
        Vector3 position=transform.position;
        position.z = zPosition;
        transform.position = position;
        // Actualizamos el tamaño de la cámara
        UpdateCameraSize();
    }
    /// <summary>
    /// Actualiza el tamaño de la cámara
    /// </summary>
    void UpdateCameraSize()
    {
        Vector2 currentMinPosition = cam.ViewportToWorldPoint(Vector2.zero);
        Vector2 currentMaxPosition = cam.ViewportToWorldPoint(Vector2.one); 
        _cameraSize = currentMaxPosition - currentMinPosition;
    }
    /// <summary>
    /// Obtiene la próxima posición a la que debe moverse la cámara
    /// </summary>
    void UpdatePosition()
    {
        Vector3 nextPosition = target.position + (Vector3)offset;
        _targetPosition = nextPosition;
        _targetPosition.z = zPosition;
    }
    /// <summary>
    /// Applica el movimiento a la cámara.
    /// </summary>
    void ApplyMovement()
    {
        transform.position = _targetPosition;
    }
    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(objectTransform.position,new Vector2(angleX,angleY));
    }*/
}
