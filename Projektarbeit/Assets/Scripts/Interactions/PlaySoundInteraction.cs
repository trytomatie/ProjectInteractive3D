using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlaySoundInteraction : InteractableObject
{
    public override void TriggerInteraction(GameObject source)
    {
        print("Ich mache Musik <3");
    }
}
