using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampInteractable : InteractableObject
{
    public GameObject targetLight;
    public Material lightMaterial;
    [ColorUsage(true,true)]
    public Color enabledEmissionColor;

    private void Start()
    {
        ToggleLight();
    }

    public override void TriggerInteraction(GameObject source)
    {
        ToggleLight();
    }

    private void ToggleLight()
    {
        targetLight.SetActive(!targetLight.activeSelf);
        if (lightMaterial != null)
        {
            if (targetLight.activeSelf) lightMaterial.SetColor("_EmissiveColor", enabledEmissionColor);
            else lightMaterial.SetColor("_EmissiveColor", Color.black);
        }
    }
}
