using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPlayAgainLogic : MonoBehaviour
{
    public GameObject panel;
    public bool play = false;
    public bool hasChosen = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChosePlay()
    {
        play = true;
        hasChosen = true;
        panel.SetActive(false);
        
    }
    public void ChoseNotPlay()
    {
        play = false;
        hasChosen = true;
        panel.SetActive(false);
    }
    public void TogglePanel(bool state)
    {
        panel.SetActive(state);
    }
}
