using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	#region Variables
	[SerializeField] private Vector3 _offset;
	private float _smoothTime = 0.25f;
	[SerializeField] private float minXBarrier = -10f;  
	[SerializeField] private float maxXBarrier = 10f;   
	[SerializeField] private float minYBarrier = -5f;   
	[SerializeField] private float maxYBarrier = 5f;
	private Vector3 _velocity;
	[SerializeField] private Transform target;
	[SerializeField] private float minYLimit;
	[SerializeField] private float maxYLimit;
	#endregion
	private void Start()
	{
		_velocity=Vector3.zero;
	}
	private void FixedUpdate()
	{
		Vector3 targetPosition = target.position + _offset;
		// Limitamos el movimiento en el eje Y
		targetPosition.y = Mathf.Clamp(targetPosition.y, minYLimit, maxYLimit);
		// limitamos el movimiento en el eje X
		targetPosition.x = Mathf.Clamp(targetPosition.x, minXBarrier, maxXBarrier);
		targetPosition.y = Mathf.Clamp(targetPosition.y, minYBarrier, maxYBarrier);
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime);
	}
}

