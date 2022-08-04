using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelPlayerOptionsLogic : MonoBehaviour
{
    public GameObject RollDiePanel;
    public GameObject RollBuildDiePanel;

    public TextMeshProUGUI RollDieText;

    // Start is called before the first frame update
    void Start()
    {
        //DisableRollDieButton();
        //DisableRollBuildDieButton();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableRollDieButton(string message) {
        SetRollDieText(message);
        RollDiePanel.SetActive(true)  ;
    }
    public void DisableRollDieButton()
    {
        RollDiePanel.SetActive(false);
    }
    public void EnableRollBuildDieButton()
    {
        RollBuildDiePanel.SetActive(true);
    }
    public void DisableRollBuildDieButton()
    {
        RollBuildDiePanel.SetActive(false);
    }

    public void SetRollDieText(string text) 
    {
        RollDieText.text = text;
    }
}
