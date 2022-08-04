using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionLogic : MonoBehaviour
{
    public GameObject camera;
    public RegionDataScriptable RegionData;
    public GameObject[] EntryRegionButtons;
    public GameObject[] Exhibits;
    public GameObject[] AdjecentNodes;
    public GameObject owner;
    // Start is called before the first frame update
    void Start()
    {
        camera.SetActive(false);
       RegionData.BuiltExhibits = 0;
        RegionData.HasOwner = false;
        DeactivateAllExhibits();
        DeactivateEntrances();
        //ActivateEntrances();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ActivateEntrances() {
        for (int i = 0; i < AdjecentNodes.Length; i++) {

            if (RegionData.BuiltExhibits > 0) {

                // AdjecentNodes[i].GetComponent<NodeLogic>().ActivateOuterEntrance();
                if (!AdjecentNodes[i].GetComponent<NodeLogic>().NodeData.HasEntry) { 
                    EntryRegionButtons[i].SetActive(true);
                    //Debug.Log("Button: " + EntryRegionButtons[i].activeSelf);  
                }   
            }
            
        }
    }
    public void DeactivateEntrances()
    {
        for (int i = 0; i < AdjecentNodes.Length; i++)
        {

                // AdjecentNodes[i].GetComponent<NodeLogic>().ActivateOuterEntrance();
                EntryRegionButtons[i].SetActive(false);
               // Debug.Log("Button: " + EntryRegionButtons[i].activeSelf);

        }
    }

  
 
    public void ActivateExhibit(int index) {
       // camera.transform.LookAt(Exhibits[index - 1].transform.Find("Object"));
        ActivateRegionCamera();
        Exhibits[index-1].transform.Find("Object").gameObject.SetActive(true);
    }
    public void DeactivateExhibit(int index)
    {
        Exhibits[index-1].transform.Find("Object").gameObject.SetActive(false);
    }

        public void DeactivateAllExhibits() {
        for (int i=1;i<=4; i++) {
            DeactivateExhibit(i);
        }
    }

    public void ActivateRegionCamera() 
    {
        camera.SetActive(true);
    }
    public void DeactivateRegionCamera()
    {
        camera.SetActive(false);
    }
}
