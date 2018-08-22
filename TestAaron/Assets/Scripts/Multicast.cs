using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multicast : MonoBehaviour {

	delegate void MultiDelegate();
	MultiDelegate multiDelegate;

	void Start () {

		multiDelegate += Print1;
		multiDelegate += Print2;

		multiDelegate();
		
	}

	void Print1(){
		Debug.Log("print 1");
	}

	void Print2(){
		Debug.Log("Print 2");
	}
}
