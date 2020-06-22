using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class PanelManager:MonoBehaviour
{
    private Dictionary<string, UITween> panels = new Dictionary<string, UITween>();
    private static PanelManager inst;
    private UITween tutpanel;
    public Text tutPanelText;
    internal static int tutorialPhase;

    void Start()
    {
        inst = this;
        UITween[] tweens = GameObject.FindObjectsOfType<UITween>();
        foreach (UITween panel in tweens)
            panels.Add(panel.name, panel);

        tutpanel = panels["Tutorial"];
       tutPanelText = tutpanel.transform.GetChild(0).GetComponent<Text>();
    }

    internal static void Show(string v)
    {
        UITween pannel;

        if (inst.panels.ContainsKey(v))
        {
            pannel = inst.panels[v];
            pannel.go = true;
        }
    }

    internal static void ShowTutorial(int v)
    {
       
        tutorialPhase = v;

        switch (v)
        {
            case 1:
                Settings.ShowSettings = false;
                Settings.SetUPAllThatStuffOnDaGround();
                Settings.SavePrefs();
                inst.tutPanelText.text = "Press Right mouse Button for Rotate camera around Point of View.\n";

                ; break;
            case 2:
                Hide("Tutorial", () =>
         {
             ShowTutorial(3);
         });
                break;
            case 3:
                inst.tutPanelText.text = "Press Wheel to pan Above Field.\n Scroll Wheel to Zoom."; inst.tutpanel.swich(); break;
            case 4:
                Hide("Tutorial", () =>
                {
                    ShowTutorial(5);
                });
                break;
            case 5:
                inst.tutPanelText.text = "Select a place and left-click to build your first Storage!"; inst.tutpanel.swich(); break;
            case 6:
                Hide("Tutorial", () =>
                {
                    //probably gameplay tutorial here
                });
                break;
            default: throw new Exception($"Not implemented tutorial phase{v}");
        }
        
        Show("Tutorial");
    }

    private static void Hide(string v, Action p)
    {
        UITween pannel;

        if (inst.panels.ContainsKey(v))
        {
            pannel = inst.panels[v];
            pannel.onComplete = p;
            pannel.swich();
            pannel.go = true;
            
        }
    }
}