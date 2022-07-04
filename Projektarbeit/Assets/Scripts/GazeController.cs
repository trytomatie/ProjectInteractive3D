using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GazeController : MonoBehaviour
{
    private Camera mainCamera;
    public float gazeDistance = 3;
    public TextMeshProUGUI commandLabel;
    public GameObject commandPanel;
    private InteractableObject currentGazedObject;
    private RectTransform contextLabelTransform;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        contextLabelTransform = commandLabel.GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Vector3 forward = mainCamera.transform.forward;
        Vector3 origin = mainCamera.transform.position;

        RaycastHit hit;

        if(Physics.Raycast(origin,forward, out hit, gazeDistance) && hit.collider.gameObject.GetComponent<InteractableObject>() != null)
        {
            commandPanel.SetActive(true);
            currentGazedObject = hit.collider.gameObject.GetComponent<InteractableObject>();
            Debug.DrawRay(origin, forward * hit.distance, Color.green, Time.fixedDeltaTime);
            commandLabel.text = currentGazedObject.commandText;
        }
        else
        {
            commandPanel.SetActive(false);
            Debug.DrawRay(origin, forward * gazeDistance, Color.red, Time.fixedDeltaTime);
            commandLabel.text = "";
            currentGazedObject = null;
        }
        LayoutRebuilder.ForceRebuildLayoutImmediate(contextLabelTransform);
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
