using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAurea : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("COLLISSION Auraaaa");
        if (other.gameObject.tag == "Player")
        {
            this.GetComponentInChildren<Animator>().SetBool("Opening", true);
        }
    }
}
