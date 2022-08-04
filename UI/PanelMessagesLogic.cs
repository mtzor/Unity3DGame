using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PanelMessagesLogic : MonoBehaviour
{

    public TextMeshProUGUI message;
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMessage(string text) {
        message.text = text;
    }

    public void ActivateMessagePanel() {

        panel.SetActive(true);

    }
    public void DeactivateMessagePanel()
    {

        panel.SetActive(false);

    }


}
