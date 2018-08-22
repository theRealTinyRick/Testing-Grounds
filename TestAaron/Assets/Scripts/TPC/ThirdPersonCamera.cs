using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

	float currentX = 0.0f;
	float currentY = 0.0f;

	const float MAX_Y = 40;
	const float MIN_Y = -30;

	[SerializeField]
	float originalCameraDistance = 6.0f;
	float currentDistance = 0;

	[SerializeField]
	Transform lookAtPos;

	void Start(){
		currentDistance = originalCameraDistance;
	}

	public void MoveCamera(float x, float y){
		currentX += x;
		currentY += y;
		
		currentY = Mathf.Clamp(currentY, MIN_Y, MAX_Y);

		Vector3 offset = new Vector3(0, 0, -currentDistance);
		Quaternion rot = Quaternion.Euler(currentY, currentX, 0);
		Vector3 tp = lookAtPos.position + rot * offset;

		transform.position = Vector3.Slerp(transform.position, tp, 0.5f);
		transform.LookAt(lookAtPos.position);
	}
}
