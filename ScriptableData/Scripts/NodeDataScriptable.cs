using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewNodeDataObj", menuName = "myObjects/NodeData")]
public class NodeDataScriptable : ScriptableObject
{
    public NodeType NodeType;
    public bool HasEntry;
    public bool HasPlayer;
    public int EntryRegion;//2 for inner region 1  for outer region
    //public GameObject EntryRegion1;

  
}
public enum NodeType
{
    Build, Buy, FreeBuild, FreeEntrance
}