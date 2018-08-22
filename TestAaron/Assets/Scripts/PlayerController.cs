using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	Rigidbody rb;
	Animator anim;

	//move vars
	[SerializeField] float speed = 5f;
	[SerializeField] float jumpHeight = 10f;
	[SerializeField] GameObject target;

	//cam vars
	Transform playerCam;
	float camX;
	float camY;
	float camDis;
	public float originalCamDis;
	public float camYMIN;
	public float camYMAX;

	bool isLockedOn = false;

	LayerMask layerMask = 1 << 8;

	bool isClimbing = false;

	void Start () {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();

		playerCam = Camera.main.transform;
		layerMask = ~layerMask;
		camDis = originalCamDis;
	}

	void Update(){
		// LockOn();
		if(Input.GetButtonDown("Jump")){
			StartCoroutine(ClimbLedge());
		}
	}

	void FixedUpdate(){
		PlayerMove();

		CameraClipping();
	}
	
	void LateUpdate () {
		if(!isLockedOn)
			CamMove();
		else
			LockOnCamera();
		
	}

	void CamMove(){
		camX += Input.GetAxis("Mouse X");
		camY += Input.GetAxis("Mouse Y");

		camY = Mathf.Clamp(camY, camYMIN, camYMAX);

		Vector3 lookAtPos = transform.position;
		lookAtPos.y += 2;

		Vector3 offset = new Vector3(0, 0, camDis);
		Quaternion rot = Quaternion.Euler(camY, camX, 0);
		Vector3 tp = lookAtPos + rot * offset;

		playerCam.position = tp;
		playerCam.LookAt(lookAtPos);
	}

	void PlayerMove(){
		if(!isClimbing){
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");
			Vector3 moveDir = new Vector3(h, 0, v);

			moveDir = playerCam.TransformDirection(moveDir);
			moveDir.y = 0;

			rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

			if(moveDir != Vector3.zero){
				Quaternion rot = Quaternion.LookRotation(moveDir);
				transform.rotation = Quaternion.Lerp(transform.rotation, rot, 0.3f);
			}
		}
	}

	void CameraClipping(){
		RaycastHit hit;;
		Vector3 o = transform.position;
		o.y += 2;
		Vector3 dir = playerCam.position - o;
		float dis = Vector3.Distance(playerCam.position, o) + 1f;

		if(Physics.Raycast(o, dir, out hit, dis, layerMask)){
			float newDis = Vector3.Distance(o, hit.point) - 0.5f;
			camDis = Mathf.Lerp(camDis, newDis, 0.4f);
		}else{
			camDis = Mathf.Lerp(camDis, originalCamDis, 0.4f);
		}
	}

	void LockOn(){
		if(Input.GetMouseButtonDown(0)){
			isLockedOn = !isLockedOn;
		}
	}

	void LockOnCamera(){
		Vector3 tp = -camDis * Vector3.Normalize(target.transform.position - transform.position) + transform.position;
		tp.y += 4;
		
		playerCam.position = tp;
		playerCam.LookAt(target.transform.position);
	}

	IEnumerator ClimbLedge(){
		Vector3 dir = transform.forward;
		Vector3 o = transform.position;
		o.y += 1;

		RaycastHit hit;
		if(Physics.Raycast(o, dir, out hit, 1, layerMask)){
			
			o.y += 2;
			if(Physics.Raycast(o, dir, out hit, 1, layerMask)){
				yield return null;
			}

			Vector3 tp = transform.position;
			tp+= transform.forward * 0.25f;

			transform.position = tp;

			//found a ledge the player can climb
			Debug.Log("hey");
			anim.Play("Climb");
			rb.isKinematic = true;
			GetComponent<CapsuleCollider>().enabled = false;
			isClimbing = true;
			yield return new WaitForSeconds(2);

			rb.isKinematic = false;
			GetComponent<CapsuleCollider>().enabled = true;
			isClimbing = false;
		}

		yield return null;
	}
}
