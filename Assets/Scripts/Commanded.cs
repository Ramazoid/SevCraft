using System;
using UnityEngine;
using Random = UnityEngine.Random;

internal class Commanded : Pointable, Informable
{
    public int ActionTime;
    public string m_ActionName;
    public int Capacity;
    public int Loaded=0;
    private Vector3 currentTargrtPoint;
    public string currentTarget;
    private Action<Transform, Vector3, Func<string, bool>> CurrentAction;
    internal Vector3 homeBase;

    void Start()
    {
        Capacity = Settings.WorkerCapacity;
    }
    internal void Command(string ActionName, string targetName, Vector3 targetPoint)
    {
        m_ActionName = ActionName;
        currentTargrtPoint = targetPoint;
        currentTarget = targetName;
        CurrentAction = Commands.getAction(ActionName);
        ActionTime = WorkerManager.getActionTime(ActionName);
        base.haveAction = true;

    }
    new private void Update()
    {
        if (CurrentAction != null)
        {
            CurrentAction(transform, currentTargrtPoint, (x) =>
             {
                 if (x == "wellDone")
                 {
                     CurrentAction = null;
                     base.haveAction = false;
                     base.SetInitPos();

                     m_ActionName = WorkerManager.getNextAction(m_ActionName);
                     ActionTime = WorkerManager.getActionTime(m_ActionName);
                     
                     CurrentAction = Commands.getAction(m_ActionName);
                     base.haveAction = true;
                 }
                 return true;
             }

                );
        }
        base.Update();
    }

    public string GetInfo()
    {
        return "<b>" + name + "</b>\n"+m_ActionName.ToUpper()+"("+ActionTime+")\n" + "Loaded:\n" + Loaded + "/" + Capacity;
    }
}