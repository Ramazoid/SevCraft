using System;
using UnityEngine;
using Random = UnityEngine.Random;

internal class Commands
{

    internal static Action<Transform, Vector3, Func<string, bool>> getAction(string actionName)
    {
        switch (actionName)
        {
            case "Go!": return MoveTo;
            case "Away!": return MoveTo;
            case "walkAround": return WalkAround;
            case "wait": return WalkAround;
            case "Mining": return MineMe;
            case "GetOFF": return GetOFF;
            case "GoNEXT": return GoNext;
            case "ToStorage": return ToStorage;
            case "UnLoading": return Unloading;

        }
        throw new NotImplementedException($"Command [[{actionName}]] not implemented");
    }
    //**********************************************************************************************MOVE TO
    private static void MoveTo(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {

        mytransform.position = Vector3.Lerp(mytransform.position, target, 0.1f);
        Vector3 v = mytransform.position;
        mytransform.position = new Vector3(v.x, 1.05f, v.z);
        if (Vector3.Distance(mytransform.position, target) < Settings.WalkAroundDistance)
        {
            retf("wellDone");
        }
        else
        {
            retf("inProgress");
        }
    } //**********************************************************************************************GetOFF//for empty mine or prohibition of access to the Storage
    private static void GetOFF(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {
        Vector3 dir = mytransform.position - target;
        target = mytransform.position + dir * Settings.GetOffDistance;
        Commanded com = mytransform.gameObject.GetComponent<Commanded>();
        com.Command("Away!", "", target);
        retf("wellDone");
        

    }
    //**********************************************************************************************GToStorage
    private static void ToStorage(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {
        Vector3 dir = mytransform.position - target;
        target = mytransform.position + dir * Settings.GetOffDistance;
        Commanded com = mytransform.gameObject.GetComponent<Commanded>();
        com.Command("Away!", "", target);
        retf("wellDone");
        

    }
    //*************************************************************************************************WALKAROUND
    private static void WalkAround(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {

        Commanded com = mytransform.gameObject.GetComponent<Commanded>();
        float randomfactor = Random.Range(0f, 1f);
        mytransform.RotateAround(target, Vector3.up, 1 * randomfactor);

        if (com.ActionTime-- > 0)

            retf("dd");
        else
            retf("wellDone");
    }//*************************************************************************************************MINE ME
    private static void MineMe(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {

        Commanded com = mytransform.gameObject.GetComponent<Commanded>();
        float randomfactor = Random.Range(0f, 1f);
        mytransform.RotateAround(target, Random.insideUnitSphere, 2 * randomfactor);

        if (com.ActionTime-- > 0)

            retf("dd");
        else
        {
            if (MineInspector.Spend(com.currentTarget))
            {
                com.Loaded++;
                com.Command("Mining", com.currentTarget, target);
            }
            else
                com.Command("GetOFF", com.currentTarget, target);
        }
    }//*************************************************************************************************Unloading
    private static void Unloading(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {

        Commanded com = mytransform.gameObject.GetComponent<Commanded>();
        float randomfactor = Random.Range(0f, 1f);
        mytransform.RotateAround(target, Random.insideUnitSphere, 2 * randomfactor);

        if (com.ActionTime-- > 0)

            retf("dd");
        else
        {
            if (com.Loaded>0)
            {
                com.Loaded--;
              
            }
            else
                com.Command("GetOFF", com.currentTarget, target);
        }
    }
    //*************************************************************************************************Go NEXT //to next Mine or home
    private static void GoNext(Transform mytransform, Vector3 target, Func<string, bool> retf)
    {

       
        Commanded com = mytransform.gameObject.GetComponent<Commanded>();
        if (com.Loaded <= com.Capacity)
        {
            Vector3 nextTarget = MineInspector.GetNextBase(target);
            com.Command("Go!", com.currentTarget, nextTarget);

        }
        else
            com.Command("ToStorage", com.currentTarget, target);
    }
}