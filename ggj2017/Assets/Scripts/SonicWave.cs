using UnityEngine;
using System.Collections;

public class SonicWave : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.transform.localScale.x < 20f)
        {
            this.gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0);
        }
	}
}
