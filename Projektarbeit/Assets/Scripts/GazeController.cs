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

    public List<InteractableObject> allInteractables;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        allInteractables = FindObjectsOfType<InteractableObject>(true).ToList();
    }

    private void FixedUpdate()
    {
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
        if(Input.GetKeyDown(KeyCode.E) && currentGazedObject != null)
        {
            currentGazedObject.TriggerInteraction(gameObject);
        }
    }
}
