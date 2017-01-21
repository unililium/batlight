using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    public GameObject shot;
    private float nextFire = 0;
    public float fireRate;

    void Start()
    {
     
    }

    void OnTriggerEnter(Collider other)
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, this.transform.position, this.transform.rotation);
            //GetComponent<AudioSource>().Play ();
        }
    }
}