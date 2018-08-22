using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSubscriber : MonoBehaviour {

	void Start () {
		EventManager.killPlayer += RegesterPlayerKilled;
	}
	
	void PrintIt(){
		Debug.Log("Event handled");
	}

	void RegesterPlayerKilled(){
		Debug.Log("Player is DEAD! The DOCTOR is DEAD! EXTERMINATE!!!: " +
		name);
	}
}
