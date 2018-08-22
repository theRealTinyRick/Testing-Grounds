using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKPractice : MonoBehaviour {

	Animator anim;
	public Transform pos;

	bool shouldIK = false;

	void Start () {
		anim = GetComponent<Animator>();

		shouldIK = true;
	}
	
	void Update () {
		
	}

	void OnAnimatorIK(){

		if(shouldIK){
			//set position and rotation weight
			anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
			anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);

			anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
			anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);

			//set position and rotation
			anim.SetIKPosition(AvatarIKGoal.LeftHand, GetPositionActual(AvatarIKGoal.LeftHand, pos.position));
			anim.SetIKRotation(AvatarIKGoal.LeftHand, transform.rotation);
			
			anim.SetIKPosition(AvatarIKGoal.RightHand, GetPositionActual(AvatarIKGoal.RightHand, pos.position));
			anim.SetIKRotation(AvatarIKGoal.RightHand, transform.rotation);
		}else{
			// anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
			// anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);

			// anim.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
			// anim.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
		}
		
	}
	
	Vector3 GetPositionActual(AvatarIKGoal goal, Vector3 pos){
		Vector3 result = pos;
		Vector3 offsetDir = Vector3.zero;

		switch(goal){
			case AvatarIKGoal.LeftHand:
				offsetDir = -transform.right;
				break;
			case AvatarIKGoal.RightHand:
				offsetDir = transform.right;
				break;
		}

		result += offsetDir * 0.5f;

		return result;
	}

	public void EndIK(){
		shouldIK = false;
	}
}
