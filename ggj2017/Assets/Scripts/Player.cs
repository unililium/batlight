using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	private Transform tr;
	private float m_horizontal;
	private float m_vertical;
	public float m_speed = 100;

	void Start()
	{
		tr = GetComponent<Transform>();
	}
	
	void Update ()
	{
		m_horizontal = Input.GetAxis ("Horizontal");
		m_vertical = Input.GetAxis ("Vertical");
	}

	void FixedUpdate()
	{
		tr.position = tr.position + m_horizontal * transform.right * m_speed * Time.fixedDeltaTime + m_vertical * transform.up * m_speed * Time.fixedDeltaTime;
	}
}
