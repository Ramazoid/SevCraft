using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInfo : MonoBehaviour
{
    private static ObjectInfo inst;
    public GameObject infomodule;
    private List<Transform> selected;
    private List<Informable> blocks = new List<Informable>();

    internal static void ShowInfo(Informable inf)
    {
        inst.blocks.Add(inf);
    }

    void Start()
    {
        inst = this;
        infomodule = Resources.Load<GameObject>("infoModule");
          
        
        infomodule.SetActive(false);
        selected = GameObject.FindObjectOfType<Pointer>().selected;
        
    }

    void Update()
    {

        if (selected.Count > 0)
        {
            blocks = new List<Informable>();
            foreach (Transform tr in selected)
            {
                Commanded c = tr.GetComponent<Commanded>();
                blocks.Add(c);
            }

            UpdateInfoPanel();
        }
        else
        {
            List<Informable> newblocks = new List<Informable>();
            foreach (Informable info in blocks)
                if (info.GetType() != typeof(Commanded))
                    newblocks.Add(info);
            blocks = newblocks;
            UpdateInfoPanel();


        }
    }
    private void UpdateInfoPanel()
    {
        
        if (transform.childCount > blocks.Count)
            for (var j = blocks.Count; j < transform.childCount; j++)
                Destroy(transform.GetChild(j).gameObject);
        
        
        int i=0;
        for (i = 0; i < blocks.Count; i++)
        {
            GameObject im;
            Vector3 pos =  new Vector3(i * 100, 0, 0);
            if (i < transform.childCount)
                im = transform.GetChild(i).gameObject;
            else
            {
                im = Instantiate(infomodule);
                RectTransform rt = im.GetComponent<RectTransform>();
                rt.SetParent(transform);
                rt.localPosition = pos;
            }
            im.name = "im" + i;
            im.GetComponent<InfoBlock>().setSource(blocks[i]);
            im.SetActive(true);
        }
        for (var k =i;k < transform.childCount;k++)
        Destroy(transform.GetChild(k).gameObject);
    }
}
