using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelActivePlayerLogic : MonoBehaviour
{
    public TextMeshProUGUI PlayerName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetActivePlayerName(string name) {
        PlayerName.text = name;
    }
}
