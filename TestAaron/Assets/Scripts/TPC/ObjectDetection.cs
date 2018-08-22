using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetection : MonoBehaviour {

	public Transform helper;
	public float offset = 0;

	Animator anim;
	LayerMask layerMask = 1 << 8;

	public bool isClimbing = false;

	void  Start(){
		anim = GetComponent<Animator>();

		layerMask = ~layerMask;
	}

	public bool HasObstical(){
		Vector3 dir = transform.forward;
		Vector3 o = transform.position;
		o.y += 1;

		RaycastHit hit;
		if(Physics.Raycast(o, dir, out hit, 3, layerMask)){
			Debug.Log("a wall is here, check for a higher ledge then slider under");
			o.y -= 0.5f;
			if(Physics.Raycast(o, dir, out hit, 3, layerMask)){
				o = transform.position;
				o.y += 3;

				if(Physics.Raycast(o, dir, out hit, 1, layerMask)){
					Debug.Log("too high");
				}else{
					//the wall is low enough to grab
					Vector3 tp = PositionToPutThePlayerOn(hit.point);
					transform.position = tp;
				}							

			}else{
				anim.Play("SlideStart");
			}
		}else{
			o.y -= 0.5f;
			if(Physics.Raycast(o, dir, out hit, 1, layerMask)){
				anim.Play("VaultStart");
			}
		}

		return false;
	}

	Vector3 PositionToPutThePlayerOn(Vector3 hit){
		Vector3 tp = hit + (transform.forward * -0.5f);
		
		Vector3 o = transform.position;
		o.y += 3;
		o += transform.forward;
		
		RaycastHit _hit;
		Debug.DrawRay(o, Vector3.down, Color.red);
		if(Physics.Raycast(o, Vector3.down, out _hit, 2, layerMask)){
			tp.y = _hit.point.y - Mathf.Abs(offset);
		} 
		return tp;
	}

	void InitClimb(){
		
	}
}
