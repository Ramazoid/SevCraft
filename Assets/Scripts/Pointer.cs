using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pointer : MonoBehaviour
{
    Queue<Vector3> track = new Queue<Vector3>();

    
    public Texture2D texture;
    static  Pointer inst;
    private Canvas canvas;
    public GameObject rama;
    public bool drawing;
    private Image im;
    private SpriteRenderer sp;
    public List<Transform> selected = new List<Transform>();

    internal static bool Checkme(Transform tr)
    {
        if (!inst.drawing)
        {
            if (inst.selected.Contains(tr))
            {
                return true;
            }
            else return false;
        }

        else
        {
            Vector3 local = Camera.main.WorldToScreenPoint(tr.position);
            if (inst.rect.Contains(local))
            {
                if (!inst.selected.Contains(tr))
                    inst.selected.Add(tr);
                return true;
            }
            else
            {
                if (inst.selected.Contains(tr))
                    inst.selected.Remove(tr);
                return false;
            }
        }
    }

    internal static void Deselect(Transform tr)
    {
        if (inst.selected.Contains(tr))
            inst.selected.Remove(tr);
    }

    public Sprite selector;
    public bool firstcornerset;
    private Vector3 leftop;
    private Vector3 rightbottom;
    public Sprite OneSelSprite;
    
    public Rect rect;
    private float buttonPressedTimer;
    private string TargetName;
    private Vector3 TargetPoint;
    private bool oneWasSelected;

    public static Sprite oneselSprite
    {
        get => inst.OneSelSprite;
        set
        {
            inst.OneSelSprite = value;
        }
    }

    public 

    void Start()
    {
        inst = this;
        canvas = GameObject.FindObjectOfType<Canvas>();

        firstcornerset = false;
        EventBus.Subscribe("Unselect", Unselect);
            
    }

    private void Unselect(object obj)
    {
        rightbottom = leftop;
        SetSelection(leftop, rightbottom);
        
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            buttonPressedTimer = Time.realtimeSinceStartup;

        if (Input.GetMouseButton(0))
        {
            if(!firstcornerset)
            {
               
                rama = new GameObject();
                rama.transform.SetParent(canvas.transform);
                rama.name = "RAMA";

                im = rama.AddComponent<Image>();
                im.sprite = selector;
                firstcornerset = true;
                leftop = Input.mousePosition;
                rightbottom = leftop; SetSelection(0, 0);
                
            }

            if (firstcornerset)
            {
                rightbottom = Input.mousePosition;

            }
            
            if (Vector3.Distance(leftop, rightbottom) > 10) drawing = true;
         

            if (drawing)
                SetSelection(leftop, rightbottom);
            
        }
        if(Input.GetMouseButtonUp(0))
        {
            SetSelection(0, 0);
            if (Time.realtimeSinceStartup - buttonPressedTimer < 1)
            {
                if (PanelManager.tutorialPhase == 5)
                    PanelManager.ShowTutorial(6);
               
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, float.PositiveInfinity, ~LayerMask.GetMask("Default")))
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Worker"))
                        SwitchSelected(hit.collider.transform);
                    else

                    {
                        oneWasSelected = false;
                        TargetName = LayerMask.LayerToName(hit.collider.gameObject.layer);
                        TargetPoint = hit.point;
                        if (PanelManager.tutorialPhase == 6)
                        {
                            PanelManager.tutorialPhase = 7;
                            StorageInspector.BuildOne(TargetPoint);
                        }
                            if (hit.collider.gameObject.name.IndexOf("Mine")!=-1)
                        InfoManager.ShowInfo(hit.collider.gameObject.GetComponent<Mine>());
                        if(hit.collider.tag=="Storage")
                            InfoManager.ShowInfo(hit.collider.gameObject.GetComponent<Storage>());
                    }
                }
            }

            drawing = false;
            firstcornerset = false;
            if (leftop == rightbottom)

                if (selected.Count != 0&& !oneWasSelected)
                    foreach (Transform tr in selected)
                        tr.GetComponent<Commanded>().Command("Go!", TargetName, TargetPoint);
               Unselect(); 
        }
    }

    private void SetSelection(int v1, int v2)
    {
        SetSelection(Vector3.zero, Vector3.zero);
    }
    private void SwitchSelected(Transform tr)
    {       
        if (!selected.Contains(tr))
        {
            selected.Add(tr);
            oneWasSelected = true;
        }
        else
            selected.Remove(tr);
    }

    private void Unselect()
    {
        rightbottom = leftop;
        SetSelection(leftop, rightbottom);
        
        drawing = false;
    }

    private void SetSelection(Vector3 leftop, Vector3 rightbottom)
    {
        
        float left = Math.Min(leftop.x, rightbottom.x);
        float width=Math.Abs(leftop.x- rightbottom.x);
        if (im != null)
        {
            im.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, left, width);

            float bott = Math.Min(leftop.y, rightbottom.y);
            float height = Math.Abs(leftop.y - rightbottom.y);
            im.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Bottom, bott, height);

            rect = new Rect(left, bott, width, height);
        }
    }
}

