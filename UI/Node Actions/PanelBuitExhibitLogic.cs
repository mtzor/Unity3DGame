using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelBuitExhibitLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // NoOwnedRegion();
       // ActivateBuildOrNotMenu();
       // DeactivateChoice3();
       // DeactivateChoice4();
       // DeactivateChoice5();
       // ShowButtons("Wax museum for: 20$","Louvre Museum for: 90$","","","");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public TextMeshProUGUI OptionsText;
    public TextMeshProUGUI[] choices;

    public GameObject button1;
    public GameObject button2;

    public GameObject[] chButtons;

  

    public GameObject panel1;
    public GameObject panel2;
    public int option;
    public bool hasChosen;
    // Start is called before the first frame update
    public bool AllButtonsDeactivated() {
        if(!chButtons[0].activeSelf && !chButtons[1].activeSelf && !chButtons[2].activeSelf && !chButtons[3].activeSelf && !chButtons[4].activeSelf && !chButtons[5].activeSelf && !chButtons[6].activeSelf)
        {
            return true;
        }
        return false;
    }
    public void ActivateBuildOrNotMenu()
    {
        panel1.SetActive(true);
    }
    public void DeactivateBuildOrNotMenu()
    {
        panel1.SetActive(false);
    }
    public void ActivateWhereToBuildMenu()
    {
        panel2.SetActive(true);
    }
    public void DeactivateWhereToBuildMenu()
    {
        panel2.SetActive(false);
    }

    public void ToggleButtons(int b1, int b2,int b3,int b4,int b5, int b6, int b7) {
        if (b1==1) {
            ΑctivateChoice(1);
        }
        else {
            DectivateChoice(1);
        }
        if (b2 == 1)
        {
            ΑctivateChoice(2);
        }
        else
        {
            DectivateChoice(2);
        }
        if (b3 == 1)
        {
            ΑctivateChoice(3);
        }
        else
        {
            DectivateChoice(3);
        }
        if (b4 == 1)
        {
            ΑctivateChoice(4);
        }
        else
        {
            DectivateChoice(4);
        }
        if (b5 == 1)
        {
            ΑctivateChoice(5);
        }
        else
        {
            DectivateChoice(5);
        }
        if (b6 == 1)
        {
            ΑctivateChoice(6);
        }
        else
        {
            DectivateChoice(6);
        }
        if (b7 == 1)
        {
            ΑctivateChoice(7);
        }
        else
        {
            DectivateChoice(7);
        }
    }
    public void ΑctivateChoice(int i)
    {
        chButtons[i-1].SetActive(true);
       
    }

    public void DectivateChoice(int i)
    {
        chButtons[i-1].SetActive(false);

    }

    public void ActivateButton1()
    {
        button1.SetActive(true);
    }
    public void DeactivateButton1()
    {
        button1.SetActive(false);
    }
 
    public void ShowButtons(string[] text)
    {
        for (int i= 0; i < 7; i++) { 
        choices[i].text = text[i];
        }
      
    }
    public void NoOwnedRegion()
    {
        option = 0;
        hasChosen = true;
        DeactivateButton1();
        OptionsText.text = "You currently do not own any regions to build on!";
        ActivateBuildOrNotMenu();
    }

    public void ShowMessage()
    {
        OptionsText.text = "You have landed on a built exhibit node. \n What would you like to do? ";
        ActivateBuildOrNotMenu();
    }
    public void ShowFreeMessage()
    {
        OptionsText.text = "You have landed on a FREE BUILD exhibit node. \n What would you like to do? ";
        ActivateBuildOrNotMenu();
    }
    public void choseBuilt()
    {
        DeactivateBuildOrNotMenu();
        if (!AllButtonsDeactivated()) {
            Debug.Log("HaveCtive Choice");
            ActivateWhereToBuildMenu();
        }
        else {
            hasChosen = true;
            option = 0;
        }
       
    }
    public void ChoseNotBuild()
    {
        option = 0;
        DeactivateBuildOrNotMenu();
        hasChosen = true;
    }

    public void chose1() {
        option = 1;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
    public void chose2()
    {
        option = 2;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
    public void chose3()
    {
        option = 3;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
    public void chose4()
    {
        option = 4;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
    public void chose5()
    {
        option = 5;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
    public void chose6()
    {
        option = 6;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
    public void chose7()
    {
        option = 7;
        hasChosen = true;
        DeactivateWhereToBuildMenu();
    }
}

