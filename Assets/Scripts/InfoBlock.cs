using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBlock : MonoBehaviour
{
    private Informable infosource;
    private Text text;

    void Start()
    {
        text = GetComponentInChildren<Text>();
        text.fontSize = 10;
    }

    void Update()
    {
        if (infosource != null)
        {
            text.text = infosource.GetInfo();
        }
        
    }

    internal void setSource(Informable com)
    {
        infosource = com;
    }
}
