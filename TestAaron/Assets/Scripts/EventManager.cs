using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public delegate void ClickAction();
	public static event ClickAction OnClicked;

	public delegate void KillPlayer();
	public static event KillPlayer killPlayer;

	void Start(){
		OnClicked = YoMa;
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Space)){
			if(killPlayer != null)
				killPlayer();
		}
	}

	void OnGUI(){
		if(GUI.Button(new Rect(Screen.width / 2 - 50, 5, 100, 30), "Click")){
			if(OnClicked != null){
				OnClicked();
			}
		}
	}

	void YoMa(){
		Debug.Log("Yo Ma Bit");
	}
}