using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelPlayerInfoLogic : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    public TextMeshProUGUI PlayerMoney;
    public TextMeshProUGUI PlayerRegions;
    public GameObject MuseumPanel;

    bool menuActive = false;
    // Start is called before the first frame update
    void Start()
    {
        PlayerRegions.text = "";
        // TogglePlayerMuseums();
        // SetPlayerMoney(20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerName(string name) {
        PlayerName.text = name;
    }
    public void SetPlayerMoney(int money)
    {
        PlayerMoney.text = money.ToString();
    }

    public void ReducePlayerMoney(int InitialMoney, int targetMoney) 
    {
        FindObjectOfType<AudioMasterLogic>().PlaySound("Money");
        currentMoney = InitialMoney;
        this.targetMoney = targetMoney;
        Invoke("ReducePlayerMoneyRepeating", 0.01f);
    }

    public void IncreasePlayerMoney(int InitialMoney, int targetMoney)
    {
        FindObjectOfType<AudioMasterLogic>().PlaySound("Money");
        currentMoney = InitialMoney;
        this.targetMoney = targetMoney;
        Invoke("IncreasePlayerMoneyRepeating", 0.01f);
    }
    int targetMoney;
    int currentMoney;
    public void ReducePlayerMoneyRepeating() {

        currentMoney = currentMoney - 1;
        PlayerMoney.text = currentMoney.ToString();
        if (currentMoney >targetMoney && currentMoney>0)
        {
            Invoke("ReducePlayerMoneyRepeating", 0.01f);
        }
        else
        {
            FindObjectOfType<AudioMasterLogic>().StopSound("Money");
        }
    }

    public void IncreasePlayerMoneyRepeating()
    {

        currentMoney = currentMoney +1;
        PlayerMoney.text = currentMoney.ToString();
        if (currentMoney < targetMoney)
        {
            Invoke("IncreasePlayerMoneyRepeating", 0.01f);
        }
        else {
            FindObjectOfType<AudioMasterLogic>().StopSound("Money");
        }
    }
    public void SetPlayerRegions(string museums)
    {
        PlayerRegions.text = museums;
    }

    public void TogglePlayerMuseums() 
    {
        menuActive = !menuActive;
        MuseumPanel.SetActive(menuActive);
    }
    public void TogglePlayerMuseums(bool state)
    {
        MuseumPanel.SetActive(state);
    }
}
