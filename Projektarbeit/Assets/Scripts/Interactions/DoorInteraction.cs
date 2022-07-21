using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorInteraction : InteractableObject
{
    public Animator anim;


    private void Start()
    {
        gameObject.isStatic = false;
    }
    public override void TriggerInteraction(GameObject source)
    {
        anim.SetBool("isFront", Vector3.Dot(transform.right, source.transform.position - transform.position) > 0);
        anim.SetBool("isOpen", !anim.GetBool("isOpen"));
    }
}
