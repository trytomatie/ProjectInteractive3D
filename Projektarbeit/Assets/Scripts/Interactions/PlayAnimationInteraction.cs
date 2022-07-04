using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayAnimationInteraction : InteractableObject
{
    public Animator anim;
    public string animationName;
    public override void TriggerInteraction(GameObject source)
    {
        anim.Play("animationName");
    }
}
