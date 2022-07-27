using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupDestroyableInteractable : PickupInteractable
{
    public float velocityToBreak = 1;

    public GameObject intactObject;
    public GameObject brokenObject;

    public Rigidbody[] brokenRbs;
    public Collider[] brokenCols;

    public GazeController holder;

    public float currentSpeed = 0;
    private void FixedUpdate()
    {
        currentSpeed = rb.velocity.magnitude;
    }

    public override void TriggerInteraction(GameObject source)
    {
        base.TriggerInteraction(source);
        holder = source.GetComponent<GazeController>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("Collision Strength: " + rb.velocity.magnitude);
        if(currentSpeed  > velocityToBreak)
        {
            BreakItem();
        }
    }

    private void BreakItem()
    {
        intactObject.GetComponent<MeshRenderer>().enabled = false;
        intactObject.GetComponent<Collider>().enabled = false;
        Rigidbody intactRb = intactObject.GetComponent<Rigidbody>();

        brokenObject.SetActive(true);
        foreach (Rigidbody rb in brokenRbs)
        {
            rb.velocity =  new Vector3(intactRb.velocity.x * Random.Range(-0.5f,0.5f), intactRb.velocity.y * Random.Range(-0.5f, 0.5f), intactRb.velocity.z * Random.Range(-0.5f, 0.5f));
        }
        intactRb.isKinematic = true;
        Invoke("DeactivateRbs", 4f);
        if(isPickedUp)
        {
            holder.DropCurrentItem();
        }
        this.enabled = false;
    }

    private void DeactivateRbs()
    {
        foreach (Rigidbody rb in brokenRbs)
        {
            rb.isKinematic = true;
        }
        foreach (Collider col in brokenCols)
        {
            col.enabled = false;
        }
    }
}
