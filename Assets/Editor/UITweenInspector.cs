using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(UITween))]
public class UITweenInspector : Editor
{
    SerializedProperty moveFrom;
    SerializedProperty moveTo;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        UITween script = (UITween)target;

        if (GUILayout.Button("Set Start"))
            script.moveFrom = script.gameObject.transform.position;
        if (GUILayout.Button("Set End"))
            script.moveTo = script.gameObject.transform.position;

        DrawDefaultInspector();
    }
}
