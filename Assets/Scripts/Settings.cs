using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Settings : MonoBehaviour
{
   
    private float f;
    private float d;
    internal static bool ShowSettings;
    internal static float WalkAroundDistance = 2;
    internal static int WorkerCapacity = 5;
    Dictionary<string, int> dic = new Dictionary<string, int>();
    object ob = new object();
    private Rect guirect;
    private static Settings inst;
    private GameObject minePrefab;
    internal static int LimitMineMax = 3;
    internal static int MineResourceMaxCapacity = 20;
    internal static int GetOffDistance = 1;
    internal static int limitWorkerMax = 5;

    internal static int WorkerMiningSpeed=10;
    private bool startbut;

    internal static int GetTime(string act)
    {
        if (inst.dic.ContainsKey(act))
            return inst.dic[act];
        else
        {
            throw new Exception($"time for action[[{act}]] wasn't defined in settings!!");
        }
    }

    internal static void SavePrefs()
    {
        PlayerPrefs.SetString("SevCraft", "saved");

        PlayerPrefs.SetInt("LimitMineMax", LimitMineMax);

        PlayerPrefs.SetInt("MineResourceMaxCapacity", MineResourceMaxCapacity);

        PlayerPrefs.SetInt("WorkerCapacity", WorkerCapacity);

        PlayerPrefs.SetInt("limitWorkerMax", limitWorkerMax);
        PlayerPrefs.SetInt("WorkerMiningSpeed", WorkerMiningSpeed);
    }


    void Start()
    {
        if (PlayerPrefs.HasKey("SevCraft") && PlayerPrefs.GetString("SevCraft") == "saved")
            LoadPrefs();
        ShowSettings = true;
        inst = this;
        minePrefab = Resources.Load<GameObject>("Mine");
      
    }

    private void LoadPrefs()
    {
        LimitMineMax=PlayerPrefs.GetInt("LimitMineMax");
        MineResourceMaxCapacity=PlayerPrefs.GetInt("MineResourceMaxCapacity");
        WorkerCapacity = PlayerPrefs.GetInt("WorkerCapacity");
        limitWorkerMax = PlayerPrefs.GetInt("limitWorkerMax");
        WorkerMiningSpeed = PlayerPrefs.GetInt("WorkerMiningSpeed");
    }
    
    public static void SetUPAllThatStuffOnDaGround()
    {
        inst.dic.Add("Mining", WorkerMiningSpeed);
        inst.dic.Add("Go!", 50000);
        inst.dic.Add("ToStorage", 100);
        inst.dic.Add("GetOFF", 1);
        inst.dic.Add("GoNEXT", 1);
        inst.dic.Add("Away!", 10);

        inst.dic.Add("walkAround", 10000);
        inst.dic.Add("UnLoadingwalkAround", 500);
        ShowSettings =false;

        for (var i = 0; i < LimitMineMax; i++)
        {
            GameObject mine = Instantiate(inst.minePrefab);
            mine.name = "Mine_" + i.ToString();
            Vector3 pos = Random.insideUnitSphere * 30;
            pos.y = 0.5f;
            
            mine.transform.position = pos;
        }
        MineInspector.Inspect();
    }

    private void OnGUI()
    {
        guirect = new Rect(130, 20, 180, 40);

        if (ShowSettings)
        {
            Texture2D back = new Texture2D(1, 1);
            back.SetPixel(0, 0, new Color(0, 0.1f, 0.8f, 0.2f));
            back.Apply();

            GUIStyle style = new GUIStyle();
            style.normal.background = back;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.UpperCenter;

            GUI.Box(new Rect(0, 0, 400, Screen.height), "               Game Settigs", style);

            GUI.Label( Re(),"LimitMineMax:"+ LimitMineMax);
            LimitMineMax = (int)GUI.HorizontalSlider(Re(), LimitMineMax, 0, 100);

            GUI.Label( Re(), "Mine.ResourceCapacity :"+MineResourceMaxCapacity);
            MineResourceMaxCapacity = (int)GUI.HorizontalSlider(Re(), MineResourceMaxCapacity, 0, 50);

            GUI.Label(Re(), "Mine.WorkerCapacity :" + WorkerCapacity);
            WorkerCapacity = (int)GUI.HorizontalSlider(Re(), WorkerCapacity, 0, 50);

            GUI.Label(Re(), "LimitWorkerMax :" + limitWorkerMax);
            limitWorkerMax = (int)GUI.HorizontalSlider(Re(), limitWorkerMax, 0, 20);

            GUI.Label(Re(), "WorkerMiningSpeed :" + WorkerMiningSpeed);
            WorkerMiningSpeed = (int)GUI.HorizontalSlider(Re(), WorkerMiningSpeed, 0, 20);


            startbut = GUI.Button(new Rect(150, Screen.height - 50, 150, 30), "Start");

            if (startbut)
            {
                ShowSettings = false;
                PanelManager.ShowTutorial(1);
            }
        }
    }

    private Rect Re()
    {
        guirect.yMin += 20;
        guirect.height = 20;
        return guirect;   
    }
}
