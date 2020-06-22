using System;

internal class WorkerManager
{
    internal static string getNextAction(string ActionName)
    {
        switch (ActionName)
        {
            case "Go!": return "walkAround";
            case "Mining": return "Mining";
            case "GetOFF": return "walkAround";
            case "Away!": return "GoNEXT";
        }

        throw new NotImplementedException($"NextAction for [[{ActionName}]] not implemented");
    }

    internal static int getActionTime(string m_ActionName)
    {
        return Settings.GetTime(m_ActionName);
    }
}