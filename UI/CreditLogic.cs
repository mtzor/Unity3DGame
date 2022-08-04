using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditLogic : MonoBehaviour
{
    public GameObject CreditPanel;
    public GameObject Player1Panel;
    public GameObject Player2Panel;

    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleCredits()
    {
        isActive = !isActive;
        CreditPanel.SetActive(isActive);
    }
    public void ToggleCredits(bool state)
    {
        CreditPanel.SetActive(state);
    }
    bool state = false;
    public void TogglePlayer1NameChoice(bool state)
    {
 
        Player1Panel.SetActive(state);
    }

    public void TogglePlayer2NameChoice(bool state)
    {
        this.state = state;
        Player2Panel.SetActive(state);
    }


}
