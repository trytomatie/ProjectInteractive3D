using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOutOfSight : MonoBehaviour
{
    public MonoBehaviour component;
    public float maxDistance;
    public float distance;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, mainCamera.transform.position);
        if (distance > maxDistance)
        {
            component.enabled = false;
        }
        else
        {
            component.enabled = true;
        }
    }



    
}
