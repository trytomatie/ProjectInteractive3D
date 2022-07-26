using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
public class GazeController : MonoBehaviour
{
    private Camera mainCamera;
    public float gazeDistance = 3;
    public LayerMask layerMask;
    private InteractableObject currentGazedObject;
    public PickupInteractable pickedUpItem;
    public float itemGrabSpeed = 3;
    private Rigidbody pickedUpItemRb;
    public List<InteractableObject> allInteractables;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        allInteractables = FindObjectsOfType<InteractableObject>(true).ToList();
    }

    private void FixedUpdate()
    {
        if (pickedUpItem != null)
        {
            return;
        }

        Vector3 forward = mainCamera.transform.forward;
        Vector3 origin = mainCamera.transform.position;

        foreach (InteractableObject interactable in allInteractables)
        {
            if(Vector3.Distance(transform.position,interactable.transform.position) < gazeDistance 
                && Vector3.Dot(transform.forward,interactable.transform.position - transform.position) > 0)
            {
                RaycastHit rh;
                Physics.Raycast(origin, interactable.transform.position - origin, out rh, gazeDistance, layerMask);
                if (rh.collider != null && rh.collider.name == interactable.name)
                {
                    interactable.IsReachable = true;
                }
                else
                {
                    interactable.IsReachable = false;
                }
                Debug.DrawRay(origin, (interactable.transform.position- origin) * rh.distance, Color.yellow, Time.fixedDeltaTime);
            }
            else
            {
                interactable.IsReachable = false;
            }
        }



        RaycastHit hit;

        if(Physics.Raycast(origin,forward, out hit, gazeDistance, layerMask) && hit.collider.gameObject.GetComponent<InteractableObject>() != null)
        {
            // commandPanel.SetActive(true);
            CurrentGazedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            CurrentGazedObject.ShowReticleText();
            Debug.DrawRay(origin, forward * hit.distance, Color.green, Time.fixedDeltaTime);
        }
        else
        {
            // commandPanel.SetActive(false);
            Debug.DrawRay(origin, forward * gazeDistance, Color.red, Time.fixedDeltaTime);
            if(CurrentGazedObject != null)
            {
                CurrentGazedObject.HideReticleText();
            }
            CurrentGazedObject = null;
        }
        //print(hit.collider.gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUpItem != null)
        {
            Vector3 direction = ((mainCamera.transform.position + mainCamera.transform.forward * 1.5f) - pickedUpItem.transform.position) * itemGrabSpeed;
            pickedUpItemRb.velocity = direction;
            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCurrentItem();
            }
            return;
        }
        if(Input.GetKeyDown(KeyCode.E) && CurrentGazedObject != null)
        {
            CurrentGazedObject.TriggerInteraction(gameObject);
        }
    }

    public void PickupItem(PickupInteractable item)
    {
        pickedUpItem = item;
        pickedUpItemRb = pickedUpItem.GetComponent<Rigidbody>();
        pickedUpItemRb.useGravity = false;
        foreach (InteractableObject interactable in allInteractables)
        {
           interactable.IsReachable = false;
        }
    }

    public void DropCurrentItem()
    {
        pickedUpItem.DropItem();
        pickedUpItemRb.useGravity = true;
        pickedUpItemRb.velocity += (mainCamera.transform.forward) * pickedUpItem.throwStrength;
        pickedUpItemRb = null;
        pickedUpItem = null;
    }


    public InteractableObject CurrentGazedObject 
    { get => currentGazedObject; 
        set 
        { 
            if(currentGazedObject != null && value != currentGazedObject)
            {
                currentGazedObject.HideReticleText();
            }
            currentGazedObject = value; 
        }
    }
}
