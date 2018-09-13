using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlerScript : MonoBehaviour
{
	private Vector3 cameraOriginPoint;
	private Vector3 offset;
	private bool isDragged;


	private void LateUpdate()
	{
		Camera.main.orthographicSize = Mathf.Clamp( Camera.main.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") 
			*(10f * Camera.main.orthographicSize *0.1f ),2.5f, 50f);

		if (Input.GetMouseButton(2))
		{
			offset = (Camera.main.ScreenToWorldPoint(Input.mousePosition)-transform.position);
			if (!isDragged)
			{
				isDragged = true;
				cameraOriginPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			}
		}
		else
		{
			isDragged = false;
		}
		if (isDragged)
		{
			transform.position = cameraOriginPoint - offset;
		}
	}
}