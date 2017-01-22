using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {

	bool activate = false;
	// Use this for initialization
	void Start () {
		EventManager.StartListening ("Sonar", Animate);

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Animator> ().SetBool ("Activate", activate);
	}
	void Animate(){
		activate = true;
	}
	void EndAnimate(){
		activate = false;
	}

}
