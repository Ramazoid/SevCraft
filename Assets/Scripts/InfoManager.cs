using System;
using UnityEngine;

internal class InfoManager
{

    internal static void ShowBaseInfo(GameObject gameObject)
    {
        
    }

    internal static void ShowInfo(Informable ob)
    {
        
        ObjectInfo.ShowInfo(ob as Informable);
    }
}