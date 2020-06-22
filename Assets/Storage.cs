using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Storage : MonoBehaviour, Informable
{

    public int CollectedResources = 0;
    private GameObject workerPrefab;
    public int WorkersAmount;
    public string GetInfo()
    {

        return $"<B>{name}</b> \n Resources:\n{CollectedResources}\n Workers:\n{WorkersAmount}";
    }

    void Start()
    {
        workerPrefab = Resources.Load<GameObject>("Worker");
        WorkersAmount = Settings.limitWorkerMax;
        for (var i = 0; i < WorkersAmount; i++)
            BurnWorker(i);
        
    }

    private void BurnWorker(int i)
    {
        GameObject worker = Instantiate(workerPrefab);
        worker.name = "Worker_" + i;
        worker.transform.position = transform.position + Random.insideUnitSphere*5;
        Vector3 v = transform.position + Random.insideUnitSphere * 5;
        worker.transform.position = new Vector3(v.x, 1.05f, v.z);
        Commanded com = worker.GetComponent<Commanded>();
        com.homeBase = transform.position;
        com.Command("GetOFF", "", transform.position);
    }

 
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider col)
    {

        Upnloading(col);

    }

    private void Upnloading(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Worker"))
        {

            Commanded worker = col.gameObject.GetComponent<Commanded>();
            
            if(worker.Loaded>0)
                worker.Command("UnLoading", name, transform.position);
            else
                worker.Command("Away!", "", Random.insideUnitSphere * 20);
        }
    }
    private void OnTriggerStay(Collider col)
    {
        Upnloading(col);
    }
}
