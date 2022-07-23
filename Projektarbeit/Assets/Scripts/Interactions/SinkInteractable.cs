using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SinkInteractable : InteractableObject
{
    public Animator handleAnim;
    public Animator waterLevelAnim;
    public ParticleSystem particleSystem;

    public float waterLevelRiseRate = 0.05f;

    private bool isOn = false;

    public override void TriggerInteraction(GameObject source)
    {
        if(isOn)
        {
            isOn = false;
            particleSystem.Stop();
            handleAnim.SetBool("IsOn",isOn);
            CancelInvoke("RiseWaterLevel");
            InvokeRepeating("LowerWaterLevel", 0.05f, 0.05f);
        }
        else
        {
            isOn = true;
            particleSystem.Play();
            handleAnim.SetBool("IsOn", isOn);
            CancelInvoke("LowerWaterLevel");
            InvokeRepeating("RiseWaterLevel", 0.05f, 0.05f);
        }
    }

    private void RiseWaterLevel()
    {

        waterLevelAnim.SetFloat("WaterLevel", waterLevelAnim.GetFloat("WaterLevel") + waterLevelRiseRate);
        if(waterLevelAnim.GetFloat("WaterLevel") >= 1)
        {
            CancelInvoke("RiseWaterLevel");
        }
    }

    private void LowerWaterLevel()
    {
        waterLevelAnim.SetFloat("WaterLevel", waterLevelAnim.GetFloat("WaterLevel") - waterLevelRiseRate);
        if (waterLevelAnim.GetFloat("WaterLevel") <= 0)
        {
            CancelInvoke("LowerWaterLevel");
        }
    }
}
