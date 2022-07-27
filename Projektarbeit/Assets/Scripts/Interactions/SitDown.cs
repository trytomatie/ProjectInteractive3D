using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitDown : InteractableObject
{
    public Transform sitPosition;
    public override void TriggerInteraction(GameObject source)
    {
        source.GetComponent<GazeController>().SitDown(sitPosition);
    }
}
