using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Poem : MonoBehaviour {

	public static int poemsCount = 0;
	private bool active = true;
	public Text[] poems;

	/// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
	{
		if(active && other.gameObject.tag == "Player")
		{
			poemsCount++;
            // read poem
			Debug.Log("Reading poem n: " + poemsCount);
			active = false;
		}
	}
}