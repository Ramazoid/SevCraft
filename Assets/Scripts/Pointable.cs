using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Pointable : MonoBehaviour
{
    public bool selected;
    private Vector3 initpos;
    internal bool haveAction;
    private GameObject mysel;
    private int myrot;

    void Start()
    {
        initpos = transform.position;
        SetInitPos();
    }

   
    public void Update()
    {
       
        selected = Pointer.Checkme(transform);

        if (selected)
        {
    
            if (mysel==null)
            {
                mysel = new GameObject();
                mysel.name = "ONESEL";
                mysel.transform.SetParent(transform);
                mysel.transform.localPosition = Vector3.zero;
                myrot = 0;
                mysel.transform.localScale = Vector3.one * 0.02f;
                SpriteRenderer sr = mysel.AddComponent<SpriteRenderer>();
                sr.sprite = Pointer.oneselSprite;
            }
            mysel.transform.localRotation = Quaternion.Euler(90, myrot++, 0);
            myrot += 5;
           
        }
        else
        {
            if (mysel != null)
                     Destroy(mysel);
        }
    }

    internal void SetInitPos()
    {
        initpos = transform.position;
    }
}
