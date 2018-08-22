using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

	Animator anim;
	Transform playerCam;
	Transform targetTransform;

	ObjectDetection objectDetection;

	string velocityX = "VelocityX";
	string velocityY = "VelocityY";

	void Start () {
		anim = GetComponent<Animator>();
		playerCam = Camera.main.transform;

		targetTransform = new GameObject().transform;
		targetTransform.name = "targetTransform";

		objectDetection = GetComponent<ObjectDetection>();
	}

	public void Move(Vector3 moveDir){


		if(anim.GetCurrentAnimatorStateInfo(0).IsName("VaultStart") ||
		anim.GetCurrentAnimatorStateInfo(0).IsName("SlideStart")){
			GetComponent<Rigidbody>().isKinematic = true;
			return;
		}else{
			GetComponent<Rigidbody>().isKinematic = false;
		}

		float actualFloat = Mathf.Max(Mathf.Abs(moveDir.x), Mathf.Abs(moveDir.z));
		anim.SetFloat(velocityY, actualFloat);

		if(actualFloat > 0){
			if(objectDetection.HasObstical()){
				return;
			}
		}

		moveDir = playerCam.TransformDirection(moveDir);
		moveDir.y = 0;

		Quaternion rot = Quaternion.LookRotation(moveDir);
		targetTransform.position = transform.position;

		if(moveDir != Vector3.zero){
			targetTransform.rotation = rot;

			float angle = Vector3.Angle(transform.forward, targetTransform.forward);
			if(Mathf.Abs(angle) > 125){
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Pivot")){
					anim.Play("Pivot");
				}
			}else{
				if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Pivot")){
					transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.4f);
				}
			}
		}
	}
}
