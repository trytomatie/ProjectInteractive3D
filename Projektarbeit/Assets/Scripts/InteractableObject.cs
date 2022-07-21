using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class InteractableObject : MonoBehaviour
{

    public string commandText;
    private bool isReachable;
    public GameObject reticle;
    private GameObject reticleInstance;
    private Animator reticleAnimator;
    private Transform canvas;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>().gameObject.transform;
    }

    public abstract void TriggerInteraction(GameObject source);

    public void Update()
    {
        
    }
    public bool IsReachable 
    { 
        get => isReachable;
        set 
        {
            if(value != isReachable)
            {
                if(value == true)
                {
                    SpawnReticle();
                }
                else
                {
                    DespawnReticle();
                }

            }
            isReachable = value;
        }
    }

    private void SpawnReticle()
    {
        if(reticleInstance == null)
        {
            reticleInstance = Instantiate(reticle, transform.position, Quaternion.identity, canvas);
            reticleInstance.GetComponent<ReticleUI>().target = transform;
            reticleAnimator = reticleInstance.GetComponent<Animator>();
        }
    }

    private void DespawnReticle()
    {
        reticleAnimator.SetTrigger("Despawn");
        Destroy(reticleInstance,0.5f);
        reticleInstance = null;
    }

    public void ShowReticleText()
    {
        reticleAnimator.SetBool("ShowText", true);
    }

    public void HideReticleText()
    {
        reticleAnimator.SetBool("ShowText", false);
    }
}
