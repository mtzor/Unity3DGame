using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ViewPanelLogic : MonoBehaviour
{
    
    public TextMeshProUGUI Infotext;
    public TextMeshProUGUI RollTextField;
    public GameObject[] Views;
    public GameObject Player1;
    bool isPressed;
    public bool wasRolled;
    public bool wasBuildRolled;
    public int FinalRoll;
    //public int FinalBuildRoll;
    public BuildDiceFaces BuildDiceResult;

   
    // Start is called before the first frame update
    void Start()
    {
        wasRolled = false;
        wasBuildRolled = false;
       
    }
}
public enum BuildDiceFaces
{
    Success,Fail,Double,Free
}
