using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverPanelLogic : MonoBehaviour
{
    public TextMeshProUGUI WinnerText;
    // Start is called before the first frame update

    public void SetWinnerText(string text) 
    {
        WinnerText.text = text;
    }
}
