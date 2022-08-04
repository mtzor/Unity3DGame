using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntranceOptionsLogic : MonoBehaviour
{
    public GameObject Camera;
    public GameObject ChoiceFreePanel;
    public GameObject ChoicePanel;
    public GameObject[] Buttons;
    public GameObject[] PlayerPanels;
    public bool hasChosen;
    public bool hasChosenPlace;
    public int placeChoice;
    public int choice;
    public RegionType Entrancetype;
    // Start is called before the first frame update
    void Start()
    {
        DeactivateChoiceFreePanel();
        DeactivateChoicePanel();
        hasChosen = false;
        hasChosenPlace = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChoseEntry(int i) {
        choice = i;
        
    }
    public void ChoseEntrytype(int i)
    {
        if (i == 0)
        {
            Entrancetype = RegionType.Outer;
        }
        else if (i == 1) {
            Entrancetype = RegionType.Inner;
        }
        hasChosen = true;
        ActivatePlayerPanels();

    }

    public void ActivatePlayerPanels()
    {
        for(int i = 0; i < PlayerPanels.Length; i++)
        {
            PlayerPanels[i].SetActive(true);
        }
    }
    public void DeactivatePlayerPanels()
    {
        for (int i = 0; i < PlayerPanels.Length; i++)
        {
            PlayerPanels[i].SetActive(false);
        }
    }
    public void ActivateCamera()
    {
        Camera.SetActive(true);
    }
    public void DeactivateCamera()
    {
        Camera.SetActive(false);
    }

    public void ChosePlaceEntrance() {
        hasChosenPlace = true;
        placeChoice = 0;

    }

    public void ChoseNotPlaceEntrance()
    {
        hasChosenPlace = true;
        placeChoice = 1;

    }
    public void ActivateChoicePanel()
    {
        ChoicePanel.SetActive(true);

    }
    public void ActivateChoiceFreePanel()
    {
        ChoiceFreePanel.SetActive(true);

    }
    public void DeactivateChoicePanel()
    {
        ChoicePanel.SetActive(false);

    }
    public void DeactivateChoiceFreePanel()
    {
        ChoiceFreePanel.SetActive(false);

    }
}
