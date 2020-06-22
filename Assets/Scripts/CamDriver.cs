using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamDriver : MonoBehaviour
{
    GameObject LookAtPoint;
    public float zoomDelta = 10;
    private Vector3 lookAtStartPos;
    public float dividor=2;
    public float farback=10;
    public float elevation=10;
    private Vector3 lookaroundAngle;
    private float storedelevation;

    void Start()
    {
        LookAtPoint = GameObject.Find("LookAtPoint");
        LookAtPoint.GetComponent<Renderer>().enabled = false;

        EventBus.Subscribe("Wheel",Zoom);
        EventBus.Subscribe("WheelPressed", StorelookAtPos);
        EventBus.Subscribe("rightPressed", StorelookAroundPos);
        EventBus.Subscribe("WheelPressedMoving", MoveLookTarget);
        EventBus.Subscribe("rightPressedMoving", MoveLookAroundTarget);
    }

    private void MoveLookAroundTarget(object obj)
    {
        Vector3 delta = (Vector3)obj;
        float angle = delta.x;
       elevation = storedelevation - delta.y/10;
        elevation = Math.Max(1, elevation);
        LookAtPoint.transform.rotation = Quaternion.Euler(lookaroundAngle.x, lookaroundAngle.y + angle, lookaroundAngle.z);
    }

    private void StorelookAroundPos(object obj)
    {
        lookaroundAngle = LookAtPoint.transform.rotation.eulerAngles;
        storedelevation = elevation;
    }

    private void StorelookAtPos(object obj)
    {
        lookAtStartPos = LookAtPoint.transform.position;
    }

    private void MoveLookTarget(object obj)
    {
        EventBus.Fire("Unselect");
        Vector3 offset = (Vector3)(obj)/30;
        Vector3 delta = new Vector3(-offset.x, 0,- offset.y);
        LookAtPoint.transform.position = lookAtStartPos - LookAtPoint.transform.right * offset.x - LookAtPoint.transform.forward * offset.y;
    }
    public void Zoom(object delta)
    {
        
        zoomDelta = (float)delta*5;
        if (farback > elevation * 2)
        {
            farback -= zoomDelta;
        }
        else if (elevation > farback * 2)
        {
            elevation -= zoomDelta;
        }
        else
        {
            farback -= zoomDelta;
            elevation -= zoomDelta;
        }
    }

    void Update()
    {
        transform.position = LookAtPoint.transform.position - LookAtPoint.transform.forward * farback + LookAtPoint.transform.up * elevation;
        transform.LookAt(LookAtPoint.transform);
    }
}
