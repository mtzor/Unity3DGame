using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerNameChoiceLogic : MonoBehaviour
{
    public TMP_InputField Name1Input;
    public TMP_InputField Name2Input;

    public PlayerDataScriptable P1;
    public PlayerDataScriptable P2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StoreName1()
    {
        P1.PlayerName = Name1Input.textComponent.text;
        
    }
    public void StoreName2()
    {
        P2.PlayerName = Name2Input.textComponent.text;

    }

}
