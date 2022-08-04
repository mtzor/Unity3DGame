using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeLogic : MonoBehaviour
{
    //public GameObject Node;
    public NodeDataScriptable NodeData;
    public GameObject[] AdjacentRegions;
    Transform outer_entrance;
    // Start is called before the first frame update
    void Start()
    {
        NodeData.HasPlayer = false;
        NodeData.HasEntry = false;
        NodeData.EntryRegion = 0;
        //DeactivateInnerEntrance();
        //DeactivateOuterEntrance();
        DeactivateInnerEntrance();
        DeactivateOuterEntrance();
     //  ActivateInnerEntrance("blue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateInnerEntrance(string color) {
        if (color == "blue") { 
        this.transform.Find("inner_entrance").GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }
        else
        {
            this.transform.Find("inner_entrance").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        this.transform.Find("inner_entrance").gameObject.SetActive(true);
    }
    public void ActivateOuterEntrance(string color)
    {
        if (this.transform.Find("outer_entrance")!=null) {
            if (color == "blue")
            {
                this.transform.Find("outer_entrance").GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
            }
            else
            {
                this.transform.Find("outer_entrance").GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            }
            this.transform.Find("outer_entrance").gameObject.SetActive(true);
            Debug.Log("Activated: " );
        }
    }

    public void DeactivateInnerEntrance()
    {
        this.transform.Find("inner_entrance").gameObject.SetActive(false);
    }
    public void DeactivateOuterEntrance()
    {
        if (this.transform.Find("outer_entrance") != null)
        {
            this.transform.Find("outer_entrance").gameObject.SetActive(false);
        }
    }

    
}
