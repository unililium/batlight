using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [Range(0f, 50f)]
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	 
	private float nextFire = 0;
    private float previousHomeHorizontal = 0;
    public Animator animator;
    public GameObject sonarS;
    public GameObject sonarB;
    public GameObject waveEffect;
    public GameObject revDisc;
    private bool activate;
    private bool isAlive;
	public int rotationSpeed;

    void Start()
    {
       animator = GetComponent<Animator>();
       activate = false;
        isAlive = true;
    }
	
	void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire && isAlive) 
		{
			nextFire = Time.time + fireRate;
			activate = true;
//            sonarS.GetComponent<Animator>().SetBool("ActivateReturn", activate);
//            sonarB.GetComponent<Animator>().SetBool("Activate", activate);
//            waveEffect.GetComponent<Animator>().SetBool("Activate", activate);
//            StartCoroutine(EndAnimation());

			EventManager.TriggerEvent("Sonar");
			//GetComponent<AudioSource>().Play ();
		}
		animator.SetBool ("Activate", activate);
        if (isAlive) { 
            float moveVertical = Input.GetAxis("Vertical");
            float moveHorizontal;
			moveHorizontal = Input.GetAxis("Horizontal");
			if (moveVertical != 0)
            {
                animator.SetBool("Moving", true);
                
            }
            else
            {
                animator.SetBool("Moving", false);
            }

            // We set the coordinates like this because the game goes vertically.
            if (moveVertical > 0f)
            {
                moveVertical = 1f;
            
            }
            else if (moveVertical < 0)
            {
                moveVertical = -1f;
            }

      
            previousHomeHorizontal = Mathf.Abs(moveVertical);

            float previousRotation = this.transform.rotation.eulerAngles.z;
			float newAngle = rotationSpeed * -1 * moveHorizontal + previousRotation;
            this.transform.rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
           
            float newRotation = this.transform.rotation.eulerAngles.z;
            Vector3 movement = new Vector3(-1*moveVertical * Mathf.Sin(Mathf.Deg2Rad*newRotation) ,  moveVertical * Mathf.Cos(Mathf.Deg2Rad * newRotation), 0.0f);
            //Debug.Log(newAngle);
            this.transform.position += movement * 0.1f;
            //Debug.Log(GetComponent<Rigidbody>().velocity);
        }

    }

    void onDeathEnded()
    {
         Application.LoadLevel("Loser");

    }

	void FixedUpdate ()
	{
		// Rotation effect of the shuttle. 
		//GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}

//    IEnumerator EndAnimation()
//    {
//        yield return new WaitForSeconds(2f);
//        activate = false;
//        sonarS.GetComponent<Animator>().SetBool("ActivateReturn", activate);
//        sonarB.GetComponent<Animator>().SetBool("Activate", activate);
//        waveEffect.GetComponent<Animator>().SetBool("Activate", activate);
//    }

    /// <summary>
	/// Sent when another object enters a trigger collider attached to this
	/// object (2D physics only).
	/// </summary>
	/// <param name="other">The other Collider2D involved in this collision.</param>
	void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("COLLISSION Player");
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Animator>().SetBool("Attacking", true);
            animator.SetBool("Death", true);
            isAlive = false;
        }
        if (other.gameObject.tag == "Aurea")
        {
            Debug.Log("AUREAAAAAAAAA");
            other.gameObject.GetComponentInChildren<Animator>().SetBool("Opening", true);
            isAlive = false;
        }
    }

    IEnumerator DoSomething(float f)
    {
        yield return new WaitForSeconds(f);

    }
	void EndAnimation(){
		activate = false;
		animator.SetBool ("Activate", activate);
		Debug.Log ("endanimation");
	}
}
