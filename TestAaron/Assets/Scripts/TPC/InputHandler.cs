using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour {

	ThirdPersonCamera playerCam;
	PlayerMove playerMove;
	ObjectDetection objectDetection;

	void Start () {
		playerCam = Camera.main.GetComponent<ThirdPersonCamera>();
		playerMove = GetComponent<PlayerMove>();
		objectDetection = GetComponent<ObjectDetection>();
	}
	
	void Update () {
		MoveInput();
	}

	void LateUpdate(){
		CameraInput();
	}

	void FixedUpdate(){
	}

	void CameraInput(){
		float h = Input.GetAxis("Mouse X");
		float v = Input.GetAxis("Mouse Y");
		
		playerCam.MoveCamera(h, v);
	}

	void MoveInput(){
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");
		Vector3 moveDir = new Vector3(h, 0, v);

		playerMove.Move(moveDir);
	}
}
