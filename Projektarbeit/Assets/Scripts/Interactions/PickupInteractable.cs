using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupInteractable : InteractableObject
{
    private bool isPickedUp;
    public float throwStrength = 0;

    private void Start()
    {
        gameObject.isStatic = false;
    }
    public override void TriggerInteraction(GameObject source)
    {
        isPickedUp = true;
        source.GetComponent<GazeController>().PickupItem(this);
    }

    public void DropItem()
    {
        isPickedUp = false;
    }
}
