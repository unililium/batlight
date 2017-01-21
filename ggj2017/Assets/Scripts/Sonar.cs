using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour {
	Animator radarAnim;
	public Animator returnAnim;
	bool activate = true;
	// Use this for initialization
	void Start () {
		radarAnim = GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			radarAnim.SetBool ("Activate", activate);
			returnAnim.SetBool ("ActivateReturn", activate);
		}
	}
}
