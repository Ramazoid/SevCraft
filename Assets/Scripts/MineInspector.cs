using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

internal class MineInspector : MonoBehaviour
{
    Dictionary<string, Mine> mines = new Dictionary<string, Mine>();
    private static MineInspector inst;

    private void Start()
    {
        inst = this;
        
    }
    internal static bool Spend(string mineName)
    {

        if (inst.mines.ContainsKey(mineName))
        {
            Mine mine = inst.mines[mineName];

            if (mine.ResourceCapacity > 0)
            {
                mine.ResourceCapacity--;
                return true;
            }
            else
                return false;
        }
        else
            throw new Exception($"Not founded {mineName} Mine!");
    }


    internal static void Inspect()
    {
        Mine[] minesScr = GameObject.FindObjectsOfType<Mine>();

        foreach (Mine mine in minesScr)
            if(!inst.mines.ContainsKey(mine.name))
            inst.mines.Add(mine.name, mine);
    }

    internal static Vector3 GetNextBase(Vector3 target)
    {
        Mine nearestMine = inst.mines.ElementAt(0).Value;
        float minDist = 100;

        foreach (Mine mine in inst.mines.Values)
            if (mine != nearestMine && Vector3.Distance(mine.transform.position, target) < minDist)
                nearestMine = mine;

        return nearestMine.transform.position;
    }
}