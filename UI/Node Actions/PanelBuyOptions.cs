using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBuyOptions : MonoBehaviour
{
    public TextMeshProUGUI Message;
    public TextMeshProUGUI Choice1;
    public TextMeshProUGUI Choice2;
    public GameObject button1;
    public GameObject button2;
    public GameObject panel;
    public int option;
    public bool hasChosen;
    // Start is called before the first frame update
    void Start()
    {
        hasChosen = false;
        option = 0;
        //DeactivateButton1();
        // ActivateBuyMenu();
        //ShowButtons("Region 1","Region7");
        //DeactivateBuyMenu();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowButtons(string text1,string text2) {
        Choice1.text = text1; 
        Choice2.text = text2;
    }
    public void SetMessage(string text) {
        Message.text = text;
    }
    public void ActivateBuyMenu() {
        panel.SetActive(true);
    }
    public void DeactivateBuyMenu()
    {
        panel.SetActive(false);
    }
    public void DeactivateButton1()
    {
        button1.SetActive(false);
    }
    public void DeactivateButton2()
    {
        button2.SetActive(false);
    }
    public void ActivateButton1()
    {
        button1.SetActive(true);
    }
    public void ActivateButton2()
    {
        button2.SetActive(true);
    }
    public void option1() {
        option = 1;
        DeactivateBuyMenu();
        hasChosen = true;
    }
    public void option2()
    {
        option = 2;
        DeactivateBuyMenu();
        hasChosen = true;
    }

    public void option3()
    {
        option = 3;
        DeactivateBuyMenu();
        hasChosen = true;
    }
}
