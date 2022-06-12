using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMaterialChange : MonoBehaviour
{

    public Material changedMaterial;
    private Material oldMaterial;
    public float speed = 0.125f;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        oldMaterial = GetComponent<MeshRenderer>().material;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
      //  rb.velocity = new Vector3(0, speed, 0);
        // transform.position += new Vector3(0, speed, 0);
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (GetComponent<MeshRenderer>().material == oldMaterial)
        {
            GetComponent<MeshRenderer>().material = changedMaterial;
        }
        else
        {
            GetComponent<MeshRenderer>().material = oldMaterial;
        }

    }

    
}
