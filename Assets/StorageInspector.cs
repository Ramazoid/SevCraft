using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInspector : MonoBehaviour
{
    private static StorageInspector inst;
    private GameObject StoragePrefab;

    void Start()
    {
        inst = this;
        StoragePrefab = Resources.Load<GameObject>("Storage");
    }

  
    void Update()
    {
        
    }

    internal static void BuildOne(Vector3 pos)
    {
        GameObject storage = Instantiate(inst.StoragePrefab);
        storage.transform.position = pos;
    }
}
