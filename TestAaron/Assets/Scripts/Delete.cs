using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete : MonoBehaviour {

	Rigidbody rb;
	float speed = 5;

	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate () {
		float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis("Vertical");

		Vector3 dir = new Vector3(h, 0 , v);
		dir = Camera.main.transform.TransformDirection(dir);
		dir.y = 0;

		rb.velocity = new Vector3(dir.x * speed, rb.velocity.y, dir.z * speed);
	}
}
