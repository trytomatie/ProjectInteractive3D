using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInteraction : InteractableObject
{
    public Animator anim;
    public bool animationCollision = false;
    private Collider coll;

    private void Start()
    {
        gameObject.isStatic = false;
        coll = GetComponent<Collider>();
    }
    public override void TriggerInteraction(GameObject source)
    {
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
        
        if(animationCollision == false)
        {
            coll.isTrigger = true;
            Invoke("SetColliderSolid", 0.5f);
        }    
    }

    public void SetColliderSolid ()
    {
        coll.isTrigger = false;
    }
}
