using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    public Transform seconds;
    public Transform minutes;
    public Transform hours;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateClock", 0, 1);
    }

    public void UpdateClock()
    {
        DateTime time = DateTime.Now;
        seconds.transform.localEulerAngles = new Vector3(0, 0, 360 / 60 * time.Second);
        minutes.transform.localEulerAngles = new Vector3(0, 0, 360 / 60 * time.Minute + (360 / 60 * (360 / 60 * time.Second) / 360));
        hours.transform.localEulerAngles = new Vector3(0, 0,360 / 12 * time.Hour + (360 / 60 * (360 / 60 * time.Minute) / 360));
    }

}
