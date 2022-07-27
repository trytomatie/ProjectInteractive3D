using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HoopScript : MonoBehaviour
{
    public TextMeshPro counterText;

    private int counter = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            counter++;
            counterText.text = counter.ToString("00");
        }
    }
}
