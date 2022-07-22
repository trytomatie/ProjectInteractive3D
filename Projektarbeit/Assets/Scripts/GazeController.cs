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
    public GameObject commandPanel;
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
            Vector3 direction = ((mainCamera.transform.position + mainCamera.transform.forward * 1.5f) - pickedUpItem.transform.position) * itemGrabSpeed;
            pickedUpItemRb.velocity = direction;
            return;
        }


        foreach(InteractableObject interactable in allInteractables)
        {
            if(Vector3.Distance(transform.position,interactable.transform.position) < gazeDistance)
            {
                interactable.IsReachable = true;
            }
            else
            {
                interactable.IsReachable = false;
            }
        }

        Vector3 forward = mainCamera.transform.forward;
        Vector3 origin = mainCamera.transform.position;

        RaycastHit hit;

        if(Physics.Raycast(origin,forward, out hit, gazeDistance) && hit.collider.gameObject.GetComponent<InteractableObject>() != null)
        {
            // commandPanel.SetActive(true);
            currentGazedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            currentGazedObject.ShowReticleText();
            Debug.DrawRay(origin, forward * hit.distance, Color.green, Time.fixedDeltaTime);
        }
        else
        {
            // commandPanel.SetActive(false);
            Debug.DrawRay(origin, forward * gazeDistance, Color.red, Time.fixedDeltaTime);
            if(currentGazedObject != null)
            {
                currentGazedObject.HideReticleText();
            }
            currentGazedObject = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pickedUpItem != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DropCurrentItem();
            }
            return;
        }
        if(Input.GetKeyDown(KeyCode.E) && currentGazedObject != null)
        {
            currentGazedObject.TriggerInteraction(gameObject);
        }
    }

    public void PickupItem(PickupInteractable item)
    {
        pickedUpItem = item;
        pickedUpItemRb = pickedUpItem.GetComponent<Rigidbody>();
        pickedUpItemRb.useGravity = false;
    }

    public void DropCurrentItem()
    {
        pickedUpItem.DropItem();
        pickedUpItemRb.useGravity = true;
        pickedUpItemRb = null;
        pickedUpItem = null;
    }
}
