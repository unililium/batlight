using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{


    void Start()
    {
     
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Ciao");
    }
}