using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteLogic : MonoBehaviour
{
    Transform[] NodesPosition;
    public List<Transform> NodePositionList = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        FillList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FillList() {//filling ist with all positions of nodes
        NodePositionList.Clear();
        NodesPosition = GetComponentsInChildren<Transform>();

        foreach (Transform nodepos in NodesPosition) {

            if (nodepos != this.transform) {

                NodePositionList.Add(nodepos);

            }
        }
    }
}
