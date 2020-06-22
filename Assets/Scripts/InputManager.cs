using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    Dictionary<string, List<Action<object>>> events;
   
    private bool wheelWaspressed;
   
    private Vector3 wheePressStartlpoint;

    private void Awake()
    {

    }
    void Start()
    {
        
    }

    void Update()
    {

        float wheel = Input.GetAxis("Mouse ScrollWheel");
        if (wheel != 0)
            EventBus.Fire("Wheel", wheel);

        if (Input.GetMouseButtonDown(2))
        {
            if (PanelManager.tutorialPhase == 3)
                PanelManager.ShowTutorial(4);
            wheelWaspressed = true;
            wheePressStartlpoint = Input.mousePosition;
            EventBus.Fire("WheelPressed", wheePressStartlpoint);
        }

        if(Input.GetMouseButton(2))
            if(wheelWaspressed)
            {
                Vector3 WheelPressedDelta = Input.mousePosition - wheePressStartlpoint;
                EventBus.Fire("WheelPressedMoving", WheelPressedDelta);
            }
        if (Input.GetMouseButtonDown(1))
        {
            if (PanelManager.tutorialPhase == 1)
                PanelManager.ShowTutorial(2);
            wheePressStartlpoint = Input.mousePosition;
            EventBus.Fire("rightPressed", wheePressStartlpoint);
        }
        if (Input.GetMouseButton(1))
            {
                Vector3 rightPressedDelta = Input.mousePosition - wheePressStartlpoint;
                EventBus.Fire("rightPressedMoving",rightPressedDelta);
            }

    }

}
