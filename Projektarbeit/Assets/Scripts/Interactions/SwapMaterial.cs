using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMaterial : InteractableObject
{
    public Material[] materials;
    public MeshRenderer[] renderers;

    public int currentIndex;


    public override void TriggerInteraction(GameObject source)
    {
        ChangeMaterial(currentIndex + 1);
    }

    public void ChangeMaterial (int newMaterialIndex)
    {
        if(newMaterialIndex >= materials.Length)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex = newMaterialIndex;
        }

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material = materials[currentIndex];
        }
    }
}
