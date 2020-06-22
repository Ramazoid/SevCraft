using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UITween : MonoBehaviour
{
    Transform tr;
    RectTransform rtr;
    public Vector3 moveFrom;
    public Vector3 moveTo;
    public EasingType easingType;
    [Range(0,1)]
    public float f;
    public bool copyfrom;
    public bool copyTo;
    public bool go;
    public int progress;
    public float ff;
    public int waitor = 0;
    public bool wait = false;
    internal Action onComplete;

    delegate float AAA(float k);
    void Start()
    {
        tr = transform;
        rtr = gameObject.GetComponent<RectTransform>();
        
    }

    
    void Update()
    {
        if (copyfrom)
        {
            copyfrom = false;
            moveFrom = transform.position;
        }
        if(copyTo)
        {
            copyTo = false;
            moveTo = transform.position;
        }

       
        Vector3 truefrom = moveFrom;
        Vector3 trueto = moveTo;
        Vector3 delta = moveTo - moveFrom;
        
       
        ff =Easing.GetByName(easingType.ToString(),f);
        float truef = ff;
        
        if(ff>1)
        {
            truefrom = moveTo; ;
            trueto = moveTo + delta;
            truef -= 1;
        }
        if(ff<0)
        {
            trueto = truefrom;
            truefrom = truefrom - delta;
            truef += 1;
        }
        if (go)
        {
            transform.position = Vector3.Lerp(truefrom, trueto, truef);
            if (f < 1)
                f += +0.001f * (++progress);
            else
            {
                go = false;
             
                progress = 0;
               f = 0;
                if (onComplete != null)
                {
                    onComplete();
                    onComplete = null;
                }
            }
        }
        else
        {
           
        }
    }

    internal void swich()
    {
        Vector3 v = moveFrom;
        moveFrom = moveTo;
        moveTo = v;
        go = true;
    }
}
