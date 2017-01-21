using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FromDarkToLight : MonoBehaviour {

    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        sprite = this.GetComponent<SpriteRenderer>();
        this.sprite.color = new Color(0.2f, 0.2f, 0.2f); 
	}
	
	// Update is called once per frame
	void Update () {
        this.sprite.color += new Color(0.01f, 0.01f, 0.01f);
    }
}
