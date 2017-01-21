using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire = 0;
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, this.transform.position, this.transform.rotation);
			//GetComponent<AudioSource>().Play ();
		}

        float moveVertical = Input.GetAxis("Vertical");
        float moveHorizontal;
        if (moveVertical != 0)
        {
            moveHorizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            moveHorizontal = 0f;
        }
        // We set the coordinates like this because the game goes vertically.
        
        float previousRotation = this.transform.rotation.eulerAngles.z;
        float newAngle = 2 * -1 * moveHorizontal + previousRotation;
        this.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        float newRotation = this.transform.rotation.eulerAngles.z;
        Vector3 movement = new Vector3(-1*moveVertical * Mathf.Sin(Mathf.Deg2Rad*newRotation) ,  moveVertical * Mathf.Cos(Mathf.Deg2Rad * newRotation), 0.0f);
        Debug.Log(newAngle);
        GetComponent<Rigidbody>().velocity = movement * speed;
        Debug.Log(GetComponent<Rigidbody>().velocity);
    }

	void FixedUpdate ()
	{

		
		
		// Rotation effect of the shuttle. 
		//GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
