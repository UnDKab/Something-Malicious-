using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Arrow : MonoBehaviour
{
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.centerOfMass = new Vector3(0, 0, 5);
        }
        else
        {
            Debug.LogError("Rigidbody component not found!");
        }
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (rb != null)
        {
            rb.isKinematic = true;
            transform.parent = collision.transform;
        }
        else
        {
            Debug.LogError("Rigidbody component not found!");
        }
    }
}
